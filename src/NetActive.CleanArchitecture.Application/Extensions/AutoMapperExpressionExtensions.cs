namespace NetActive.CleanArchitecture.Application.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;

    using AutoMapper;

    using Domain.Interfaces;

    using Interfaces;

    /// <summary>
    /// AutoMapper related extension methods.
    /// </summary>
    public static class AutoMapperExpressionExtensions
    {
        /// <summary>
        /// Maps from a Translation entity using the CurrentThread CurrentCulture, with a fallback to the first translation record available.
        /// </summary>
        /// <typeparam name="TSource">The type of source <see cref="IEntity"/> with Translations navigation property containing the translations for each filled language.</typeparam>
        /// <typeparam name="TSourceMember">Type of source member.</typeparam>
        /// <typeparam name="TDestination">The type of destination to map to.</typeparam>
        /// <typeparam name="TMember">The type of the property to be translated.</typeparam>
        /// <typeparam name="TTranslation">Type of translation.</typeparam>
        /// <param name="expression">The previous expression to extend to.</param>
        /// <param name="sourceMember">The property of the source entity containing all filled translations.</param>
        /// <param name="translationMember">The property of the current translation to get.</param>
        /// <returns>The mapping expression we've extended from.</returns>
        public static IMemberConfigurationExpression<TSource, TDestination, TMember>
            MapFromTranslation<TSource, TDestination, TMember, TSourceMember, TTranslation>(
                this IMemberConfigurationExpression<TSource, TDestination, TMember> expression,
                Expression<Func<TSource, ICollection<TSourceMember>>> sourceMember,
                Expression<Func<TSourceMember, TTranslation>> translationMember)
            where TSource : class, IEntity
            where TSourceMember : class, IEntityTranslation
            where TDestination : IModel
        {
            var cultureName = Thread.CurrentThread.CurrentCulture.Name;
            var languageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // https://stackoverflow.com/a/19434133/3883467
            Expression<Func<ICollection<TSourceMember>, TSourceMember>> translationExpression = s =>
                s.FirstOrDefault(t => t.Culture.Equals(cultureName))
                ?? s.FirstOrDefault(t => t.Culture.Equals(languageName)) ?? s.FirstOrDefault();

            var param = Expression.Parameter(typeof(TSource), "param");

            var newSourceMember = new ReplaceVisitor(sourceMember.Parameters.First(), param).Visit(sourceMember.Body);
            var newTranslationExpression =
                new ReplaceVisitor(translationExpression.Parameters.First(), newSourceMember).Visit(
                    translationExpression.Body);
            var newTranslationMember =
                new ReplaceVisitor(translationMember.Parameters.First(), newTranslationExpression).Visit(
                    translationMember.Body);

            var finalMerged = Expression.Lambda<Func<TSource, TMember>>(newTranslationMember, param);

            expression.MapFrom(finalMerged);

            return expression;
        }

        /// <summary>
        /// Used internally to visit an expression, so merging of expressions can be implemented.
        /// </summary>
        internal class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _from, _to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == _from ? _to : base.Visit(node);
            }
        }
    }
}