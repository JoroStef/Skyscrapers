using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Skyscrapers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skyscrapers.Data
{
    public static class SqlFunctions
    {
        public static string ToString(this StatusType? value, int? length, int? decimalArg) => throw new NotSupportedException();

        public static ModelBuilder AddSqlFunctions(this ModelBuilder modelBuilder) => modelBuilder
            .MapToSTR(() => ToString(default(StatusType?), null, null));
            
        static ModelBuilder MapToSTR(this ModelBuilder modelBuilder, Expression<Func<string>> method)
        {
            modelBuilder.HasDbFunction(method).HasTranslation(args =>
            new SqlFunctionExpression("STR", true, typeof(string), null));
                //new SqlFunctionExpression(null, null, "STR", false, args, true, typeof(string), null));
            return modelBuilder;
        }
    }
}
