// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

//ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class MySqlDateTimeDatePartComponentTranslator : IMemberTranslator
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual Expression Translate(MemberExpression memberExpression)
        {
            string datePart;
            if (memberExpression.Expression != null
                && (memberExpression.Expression.Type == typeof(DateTime) || memberExpression.Expression.Type == typeof(DateTimeOffset))
                && (datePart = GetDatePart(memberExpression.Member.Name)) != null)
            {
                return new SqlFunctionExpression(
                    functionName: "EXTRACT",
                    returnType: memberExpression.Type,
                    arguments: new[]
                    {
                        new SqlFragmentExpression($"{datePart} FROM {memberExpression.Expression}")
                    });
            }
            return null;
        }

        private static string GetDatePart(string memberName)
        {
            switch (memberName)
            {
                case nameof(DateTime.Year):
                    return "year";
                case nameof(DateTime.Month):
                    return "month";
                case nameof(DateTime.Day):
                    return "day";
                case nameof(DateTime.Hour):
                    return "hour";
                case nameof(DateTime.Minute):
                    return "minute";
                case nameof(DateTime.Second):
                    return "second";
                default:
                    return null;
            }
        }
    }
}
