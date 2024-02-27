using Azure;
using System.Text;
using System.Text.Json.Serialization;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public abstract class SecurityPolicyPredicate
    {
        public string FunctionSchema { get; set; } = "security";
        public required string FunctionName { get; set; }
        public string TargetTableSchema { get; set; } = "dbo";
        public required string TargetTableName { get; set; }
        public string TargetTableColumn { get; set; } = "Id";
        public required string Policy { get; set; }

        public abstract string RenderCreateSql();
    }

    public class SecurityPolicyFilterPredicate : SecurityPolicyPredicate
    {
        public override string RenderCreateSql()
        {
            return $"ADD FILTER PREDICATE [{FunctionSchema}].[{FunctionName}]({TargetTableColumn}, '{Policy}') ON [{TargetTableSchema}].[{TargetTableName}]";
        }
    }

    public sealed class SecurityPolicyBlockPredicateOperation
    {
        public string Operation { get; }

        [JsonConstructor]
        private SecurityPolicyBlockPredicateOperation(string operation) => Operation = operation;

        public static SecurityPolicyBlockPredicateOperation All => new SecurityPolicyBlockPredicateOperation("");
        public static SecurityPolicyBlockPredicateOperation AfterInsert => new SecurityPolicyBlockPredicateOperation("AFTER INSERT");
        public static SecurityPolicyBlockPredicateOperation AfterUpdate => new SecurityPolicyBlockPredicateOperation("AFTER UPDATE");
        public static SecurityPolicyBlockPredicateOperation BeforeUpdate => new SecurityPolicyBlockPredicateOperation("BEFORE UPDATE");
        public static SecurityPolicyBlockPredicateOperation BeforeDelete => new SecurityPolicyBlockPredicateOperation("BEFORE DELETE");
    }

    public class SecurityPolicyBlockPredicate : SecurityPolicyPredicate
    {
        public required SecurityPolicyBlockPredicateOperation Operation { get; set; }

        public override string RenderCreateSql()
        {
            return $"ADD BLOCK PREDICATE [{FunctionSchema}].[{FunctionName}]({TargetTableColumn}, '{Policy}') ON [{TargetTableSchema}].[{TargetTableName}] {Operation.Operation}";
        }
    }

    public class SecurityPolicy
    {
        public static string Annotation => "securitypolicy";

        public string Schema { get; set; } = "security";
        public required string Name { get; set; }

        public SecurityPolicyFilterPredicate? FilterPredicate { get; set; }
        public List<SecurityPolicyBlockPredicate> BlockPredicates { get; set; } = new List<SecurityPolicyBlockPredicate>();

        public static SecurityPolicy? FromJsonString(string json) => System.Text.Json.JsonSerializer.Deserialize<SecurityPolicy>(json);

        public string ToJson() => System.Text.Json.JsonSerializer.Serialize(this);

        public SecurityPolicy AddFilterPredicate(SecurityPolicyFilterPredicate predicate)
        {
            FilterPredicate = predicate;
            return this;
        }

        public SecurityPolicy AddFilterPredicate(string functionName, string tableName, string policy)
        {
            AddFilterPredicate(new SecurityPolicyFilterPredicate { FunctionName = functionName, TargetTableName = tableName, Policy = policy });
            return this;
        }

        public SecurityPolicy AddBlockPredicate(SecurityPolicyBlockPredicate predicate)
        {
            BlockPredicates.Add(predicate);
            return this;
        }

        public SecurityPolicy AddBlockPredicate(string functionName, string tableName, string policy, SecurityPolicyBlockPredicateOperation operation)
        {
            AddBlockPredicate(new SecurityPolicyBlockPredicate { FunctionName = functionName, TargetTableName = tableName, Policy = policy, Operation = operation });
            return this;
        }

        public string RenderCreateSql()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"CREATE SECURITY POLICY [{Schema}].[{Name}]");
            if (FilterPredicate != null) builder.AppendLine($" {FilterPredicate.RenderCreateSql()},");
            BlockPredicates.ForEach(predicate => builder.AppendLine($" {predicate.RenderCreateSql()},"));
            builder.AppendLine($" WITH (STATE = ON);");

            var script = builder.ToString();

            int index = script.LastIndexOf(',');
            script = script.Remove(index, 1);

            return script;
        }
    }
}