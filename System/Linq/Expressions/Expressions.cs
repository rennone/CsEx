using System;
using System.Linq.Expressions;

namespace CsEx.SystemEx.Linq.Expressions
{
    using Expression = Expression;
    using Binary = Func<Expression, Expression, BinaryExpression>;
    using Unary = Func<Expression, UnaryExpression>;
    using TypeUnary = Func<Expression, Type, UnaryExpression>;
    internal class Expression<T>
    {
        private static readonly ParameterExpression x_ = Expression.Parameter(typeof(T), "x");
        private static readonly ParameterExpression y_ = Expression.Parameter(typeof(T), "y");

        /// <summary>
        /// T, T -> Tとなる演算(2項演算)
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static Func<T, T, T> Lambda(Binary op)
        {
            return Expression.Lambda<Func<T, T, T>>(op(x_, y_), x_, y_).Compile();
        }

        /// <summary>
        /// T, T -> Uとなる演算(2項演算)
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static Func<T, T, TU> Lambda<TU>(Binary op)
        {
            return Expression.Lambda<Func<T, T, TU>>(op(x_, y_), x_, y_).Compile();
        }

        /// <summary>
        /// T -> Tとなる演算(単項演算)
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static Func<T, T> Lambda(Unary op)
        {
            return Expression.Lambda<Func<T, T>>(op(x_), x_).Compile();
        }

        /// <summary>
        /// T -> TUとなる演算(単項演算)変換処理
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="op"></param>
        /// <returns></returns>
        public static Func<T, TU> Convert<TU>(TypeUnary op)
        {
            return Expression.Lambda<Func<T, TU>>(op(x_, typeof(TU)), x_).Compile();
        }

    }
    
    /// <summary>
    /// 足し算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Add<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.Add);
    }

    /// <summary>
    /// 引き算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Sub<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.Subtract);
    }

    /// <summary>
    /// 掛け算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Mul<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.Multiply);
    }

    /// <summary>
    /// 割り算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Div<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.Divide);
    }

    /// <summary>
    /// 単項+演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Plus<T>
    {
        public static readonly Func<T, T> body_ = Expression<T>.Lambda(Expression.UnaryPlus);
    }

    /// <summary>
    /// 単項-演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Minus<T>
    {
        public static readonly Func<T, T> body_ = Expression<T>.Lambda(Expression.Negate);
    }
    /// <summary>
    /// 任意の型に変換
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Convert<T, TU>
    {
        public static readonly Func<T, TU> body_ = Expression<T>.Convert<TU>(Expression.Convert);
    }

    /// <summary>
    /// 左シフト
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class LeftShift<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.LeftShift);
    }

    /// <summary>
    /// 右シフト
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class RightShift<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.RightShift);
    }

    /// <summary>
    /// Or演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Or<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.Or);
    }

    /// <summary>
    /// And演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class And<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.And);
    }

    /// <summary>
    /// XOr演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ExclusiveOr<T>
    {
        public static readonly Func<T, T, T> body_ = Expression<T>.Lambda(Expression.ExclusiveOr);
    }

    /// <summary>
    /// > 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GreaterThan<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.GreaterThan);
    }

    /// <summary>
    /// >= 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GreaterThanOrEqual<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.GreaterThanOrEqual);
    }

    /// <summary>
    /// < 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class LessThan<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.LessThan);
    }
    /// <summary>
    /// <= 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class LessThanOrEqual<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.LessThanOrEqual);
    }

    /// <summary>
    /// == 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Equal<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.Equal);
    }

    /// <summary>
    /// != 演算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class NotEqual<T>
    {
        public static readonly Func<T, T, bool> body_ = Expression<T>.Lambda<bool>(Expression.NotEqual);
    }
}