using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortalData.Utils
{

    static public class DapperExtensions
    {

        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            MemberExpression memberExpression;
            if (lambdaExpression.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)lambdaExpression.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambdaExpression.Body;
            }
            return memberExpression.Member;
        }

        public static TReturn GetValue<TReturn, TProperty>(this DynamicParameters dynamicParameters, Expression<Func<TProperty>> property)
        {
            return dynamicParameters.Get<TReturn>("@" + property.GetMemberInfo().Name);
        }


        //Feel free to use this as you want 
        public static DynamicParameters PrepareParams<T>(this IDbConnection connection, T entity) where T : class
        {
            var dynamicParameters = new DynamicParameters();
            var type = typeof(T);
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                ParameterTypeAttribute[] attributes = (ParameterTypeAttribute[])prop.GetCustomAttributes(
               typeof(ParameterTypeAttribute), false);

                ParameterTypeAttribute metadata;
                if (attributes != null && attributes.Length > 0)
                {
                    metadata = attributes.Single();//make sure that there are no multiple attributes 
                }
                else
                {
                    metadata = new ParameterTypeAttribute();
                }

                if (!metadata.IgnoreField)
                {
                    object value = prop.GetValue(entity);
                    dynamicParameters.Add("@" + prop.Name, value, null, metadata.Direction);
                }
            }

            return dynamicParameters;
        }
    }
}
