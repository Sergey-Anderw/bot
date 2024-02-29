using System.Linq.Expressions;

namespace Shared.Extensions
{
	public static class ExpressionExtensions
	{
		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
		{
			var parameterExpression = a.Parameters[0];
			var visitor = new SubstExpressionVisitor
			{
				Subst = { [b.Parameters[0]] = parameterExpression }
			};

			var body = Expression.AndAlso(a.Body, visitor.Visit(b.Body)!);
			return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
		{

			var parameterExpression = a.Parameters[0];
			var visitor = new SubstExpressionVisitor
			{
				Subst = { [b.Parameters[0]] = parameterExpression }
			};

			var body = Expression.OrElse(a.Body, visitor.Visit(b.Body)!);
			return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
		}

		internal class SubstExpressionVisitor : ExpressionVisitor
		{
			public Dictionary<Expression, Expression> Subst = new Dictionary<Expression, Expression>();

			protected override Expression VisitParameter(ParameterExpression node)
			{
				return Subst.TryGetValue(node, out var newValue) ? newValue : node;
			}
		}

	}
}
