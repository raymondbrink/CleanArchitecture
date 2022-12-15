namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Mapping
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using Models;

    internal static class MappingExpressions
    {
        public static Expression<Func<StoreProduct, bool>> IsAvailable =>
            product =>
                product.Product.AvailableFrom <= DateTime.UtcNow &&
                (!product.Product.AvailableUntil.HasValue || product.Product.AvailableUntil >= DateTime.UtcNow);

        public static Expression<Func<StoreProduct, bool>> IsInStock => product => product.InStock > 0;

        public static Expression<Func<StoreProduct, StoreProductStatusModel>> Status =>
            product =>
                IsAvailable.Invoke(product)
                    ? IsInStock.Invoke(product)
                        ? StoreProductStatusModel.Available
                        : StoreProductStatusModel.NotInStock
                    : StoreProductStatusModel.Unavailable;

        public static Expression<Func<StoreProduct, string>> ProductName
        {
            get
            {
                var cultureName = Thread.CurrentThread.CurrentCulture.Name;
                var languageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return s => s.Product.Translations.FirstOrDefault(e => matchesCulture(cultureName).Invoke(e)).Name ??
                            s.Product.Translations.FirstOrDefault(e => matchesCulture(languageName).Invoke(e)).Name ??
                            s.Product.Translations.FirstOrDefault().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        private static Expression<Func<ProductTranslation, bool>> matchesCulture(string culture)
        {
            return t => t.Culture == culture;
        }

        //public static Expression<Func<ICollection<ProductTranslation>, string>> FromTranslation<TTranslation>(
        //        Expression<Func<ProductTranslation, TTranslation>> translationMember)
        //{
        //    var cultureName = Thread.CurrentThread.CurrentCulture.Name;
        //    var languageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        //    // https://stackoverflow.com/a/19434133/3883467
        //    Expression<Func<ICollection<ProductTranslation>, ProductTranslation>> translationExpression = s =>
        //        s.FirstOrDefault(t => t.Culture == cultureName)
        //        ?? s.FirstOrDefault(t => t.Culture == languageName) ?? s.FirstOrDefault();

        //    var param = Expression.Parameter(typeof(ICollection<ProductTranslation>), "param");

        //    //var newSourceMember = new ReplaceVisitor(sourceMember.Parameters.First(), param).Visit(sourceMember.Body);
        //    var newTranslationExpression = Expression.Invoke(translationExpression, param);
        //        //new ReplaceVisitor(translationExpression.Parameters.First(), param).Visit(
        //        //    translationExpression.Body);
        //    var newTranslationMember = Expression.Invoke(translationMember, newTranslationExpression);
        //        //new ReplaceVisitor(translationMember.Parameters.First(), newTranslationExpression).Visit(
        //        //    translationMember.Body);

        //    return Expression.Lambda<Func<ICollection<ProductTranslation>, string>>(newTranslationMember, param);
        //}

        ///// <summary>
        ///// Used internally to visit an expression, so merging of expressions can be implemented.
        ///// </summary>
        //internal class ReplaceVisitor : ExpressionVisitor
        //{
        //    private readonly Expression _from, _to;

        //    public ReplaceVisitor(Expression from, Expression to)
        //    {
        //        _from = from;
        //        _to = to;
        //    }

        //    public override Expression Visit(Expression node)
        //    {
        //        return node == _from ? _to : base.Visit(node);
        //    }
        //}

        //public static Expression<Func<Product, object>> FromTranslation
        //{
        //    get
        //    {
        //        var cultureName = Thread.CurrentThread.CurrentCulture.Name;
        //        var languageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        //        Expression<Func<Product, ProductTranslation>> translationExpression = s =>
        //            s.Translations.FirstOrDefault(t => t.Culture == cultureName) ??
        //            s.Translations.FirstOrDefault(t => t.Culture == languageName) ??
        //            s.Translations.FirstOrDefault();

        //        Expression<Func<ProductTranslation, object>> memberExpression = t => t.Name;

        //        // TODO: Combine expressions somehow?
        //        throw new NotImplementedException();
        //    }
        //}
    }
}