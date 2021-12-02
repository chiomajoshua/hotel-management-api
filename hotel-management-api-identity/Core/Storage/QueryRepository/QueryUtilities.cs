using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Helpers.Extension;
using System.Reflection;
using System.Text;

namespace hotel_management_api_identity.Core.Storage.QueryRepository
{
    public interface IQueryUtilities : IAutoDependencyCore
    {
        string GetConnectionString();
        object GetObjectParams<TEntity>(TEntity entity) where TEntity : class;

        Dictionary<string, object> GenerateParams<TEntity>(IEnumerable<PropertyInfo> listOfProperties, TEntity entity) where TEntity : class;

        string GenerateInsertQuery<TEntity>(TEntity entity) where TEntity : class;

        string GenerateBulkInsertQuery<TEntity>(List<Dictionary<string, object>> columnValues) where TEntity : class;

        Dictionary<string, object> GenerateAuditTrailParams<TEntity>(Dictionary<string, object> objectDictionary, IEnumerable<PropertyInfo> listOfAuditLogProperties, TEntity auditLogEntity)
            where TEntity : class;

        string GenerateAuditLogInsertQuery<TEntity>(TEntity entity) where TEntity : class;

        object GetObjectParamsWithAuditLog<TEntity, TEntity1>(TEntity entity, TEntity1 auditLogEntity) where TEntity : class
            where TEntity1 : class;

        string GenerateUpdateQuery<TEntity>(TEntity entity) where TEntity : class;

        string GenerateSingleRecordQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class;

        string GenerateSelectQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class;
        string GeneratePaginatedSelectQuery<TEntity>(Dictionary<string, string> criteria, int pageSize, int pageNumber) where TEntity : class;

        string GenerateSelectQuery2<TEntity>(Dictionary<string, string> criteria) where TEntity : class;

        string GenerateDeleteQuery<TEntity>(TEntity entity) where TEntity : class;
        string GenerateBulkUpdateQuery<TEntity>(List<Dictionary<string, object>> columnValues) where TEntity : class;

        string GenerateTokenValidationQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class;

        string GenerateSingleRecordQuery<TEntity>(Dictionary<string, Guid> criteria) where TEntity : class;
    }


    public class QueryUtilities : IQueryUtilities
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;

        public QueryUtilities(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");           
        }

        public string GetConnectionString()
        {
            return _connStr;
        }

        public object GetObjectParams<TEntity>(TEntity entity) where TEntity : class
        {
            return GenerateParams(typeof(TEntity).GetProperties(), entity).ConvertToAnonymousObject();
        }

        public Dictionary<string, object> GenerateParams<TEntity>(IEnumerable<PropertyInfo> listOfProperties, TEntity entity) where TEntity : class
        {
            var objectDictionary = new Dictionary<string, object>();
            foreach (var prop in listOfProperties)
            {
                if (!entity.GetType().IgnoreProperty<Type>(prop.Name))
                {
                    objectDictionary.Add(prop.Name, value: prop.GetValue(entity));
                }
            }
            return objectDictionary;
        }

        public string GenerateInsertQuery<TEntity>(TEntity entity) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var insertQuery = new StringBuilder($"INSERT INTO dbo.[{tableName}] ");
            insertQuery.Append('(');
            var properties = GenerateParams(typeof(TEntity).GetProperties(), entity).Keys.ToList();
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(startIndex: insertQuery.Length - 1, length: 1).Append(") VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(startIndex: insertQuery.Length - 1, length: 1)
                .Append(')');
            return insertQuery.ToString();
        }

        public string GenerateBulkInsertQuery<TEntity>(List<Dictionary<string, object>> columnValues) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            string query = null;
            foreach (var listItem in columnValues)
            {
                query = $"{query}insert into dbo.[{tableName}] (";
                foreach (string item in listItem.Keys)
                {
                    query = $"{query}{item},";
                }
                query = $"{query.Substring(0, query.Length - 1)}) values( ";
                foreach (object item in listItem.Values)
                {
                    query = $"{query}'{item}',";
                }
                query = $"{query[0..^1]});";
            }
            return query;
        }

        public string GenerateUpdateQuery<TEntity>(TEntity entity) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var updateQuery = new StringBuilder($"UPDATE dbo.[{tableName}] ");
            updateQuery.Append("SET");
            var properties = GenerateParams(typeof(TEntity).GetProperties(), entity).Keys.ToList();
            foreach (var prop in properties)
            {
                if (prop == "ID") continue;
                updateQuery.Append($" [{prop}] = @{prop},");
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" where ID = @ID");
            return updateQuery.ToString();
        }


        public string GenerateDeleteQuery<TEntity>(TEntity entity) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var deleteQuery = new StringBuilder($"DELETE FROM dbo.[{tableName}] ");
            deleteQuery.Append($" where ID = @ID");
            return deleteQuery.ToString();
        }

        #region AuditLogScriptPart

        public object GetObjectParamsWithAuditLog<TEntity, TEntity1>(TEntity entity, TEntity1 auditLogEntity)
            where TEntity : class
            where TEntity1 : class
        {
            Dictionary<string, object> keyValuePairs = GenerateParams(typeof(TEntity).GetProperties(), entity);
            keyValuePairs = GenerateAuditTrailParams(keyValuePairs, typeof(TEntity1).GetProperties(), auditLogEntity);
            return keyValuePairs.ConvertToAnonymousObject();
        }

        public Dictionary<string, object> GenerateAuditTrailParams<TEntity>(Dictionary<string, object> objectDictionary, IEnumerable<System.Reflection.PropertyInfo> listOfAuditLogProperties, TEntity auditLogEntity)
            where TEntity : class
        {
            objectDictionary = objectDictionary ?? new Dictionary<string, object>();

            foreach (var prop in listOfAuditLogProperties)
            {
                if (!auditLogEntity.GetType().IgnoreTrailProperty<Type>(prop.Name))
                {
                    objectDictionary.Add($"AuditLog_{prop.Name}", prop.GetValue(auditLogEntity));
                }
            }

            return objectDictionary;
        }

        public string GenerateAuditLogInsertQuery<TEntity>(TEntity entity) where TEntity : class
        {
            var insertQuery = new StringBuilder($"INSERT INTO AuditLogs ");
            insertQuery.Append("(");
            var properties = GenerateParams(typeof(TEntity).GetProperties(), entity).Keys.ToList();
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(startIndex: insertQuery.Length - 1, length: 1)
                .Append(") VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@AuditLog_{prop},"); });
            insertQuery
                .Remove(startIndex: insertQuery.Length - 1, length: 1)
                .Append(")");

            return insertQuery.ToString();
        }

        public string GenerateSelectQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT * FROM dbo.[{tableName}] with (nolock) WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                selectQuery.Append($"{item.Key} like '%{item.Value.Trim().Replace(' ', '%')}%' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }
            return $"{selectQuery} order by createdon desc";
        }

        public string GenerateSingleRecordQuery<TEntity>(Dictionary<string, Guid> criteria) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT TOP 1 ID FROM dbo.[{tableName}] WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                selectQuery.Append($"{item.Key} = '{item.Value}' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }
            return $"{selectQuery} order by CreatedOn desc";
        }

        public string GenerateTokenValidationQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT * FROM dbo.[{tableName}] with (nolock) WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                 selectQuery.Append($"{item.Key} like '%{item.Value.Trim().Replace(' ', '%')}%' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }

            return $"{selectQuery} AND ExpiryDate >= {DateTimeOffset.UtcNow} order by createdon desc";
        }
        public string GeneratePaginatedSelectQuery<TEntity>(Dictionary<string, string> criteria, int pageSize, int pageNumber) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT * FROM dbo.[{tableName}] with (nolock) WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                selectQuery.Append($"{item.Key} like '%{item.Value.Trim().Replace(' ', '%')}%' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }
            return $"{selectQuery} order by createdon desc OFFSET {pageNumber} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public string GenerateSelectQuery2<TEntity>(Dictionary<string, string> criteria) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT * FROM dbo.[{tableName}] with (nolock) WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                selectQuery.Append($"{item.Key} like '%{item.Value.Trim().Replace(' ', '%')}%' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }
            return $"{selectQuery}";
        }

        public string GenerateSingleRecordQuery<TEntity>(Dictionary<string, string> criteria) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            var selectQuery = new StringBuilder($"SELECT TOP 1 ID FROM dbo.[{tableName}] WHERE ");
            int count = 1;
            foreach (var item in criteria)
            {
                selectQuery.Append($"{item.Key} = '{item.Value}' ");
                if (criteria.Count > count)
                {
                    selectQuery.Append("AND ");
                }
                count++;
            }
            return $"{selectQuery} order by CreatedOn desc";
        }

        public string GenerateBulkUpdateQuery<TEntity>(List<Dictionary<string, object>> columnValues) where TEntity : class
        {
            string tableName = typeof(TEntity).GetTableName<Type>();
            string query = null;
            string idString = "";
            foreach (var listItem in columnValues)
            {
                string tempQuery = query;
                query = $"{query}update dbo.[{tableName}] set ";
                foreach (string item in listItem.Keys)
                {
                    if (item.ToLower() == "id")
                    {
                        idString = $" where id = '{listItem[item]}'";
                    }
                    query = $"{query}{item} = '{listItem[item]}',";
                }
                query = !string.IsNullOrEmpty(idString) ? $"{query.Substring(0, query.Length - 1)}{idString};{Environment.NewLine}" : tempQuery;
                idString = "";
            }
            return query;
        }

        #endregion
    }
}