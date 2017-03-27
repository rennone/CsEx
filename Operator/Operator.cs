﻿using System;
using System.Linq.Expressions;

// http://ufcpp.net/study/csharp/sm_genericop.html
namespace CsEx.Operator
{
    using Binary = Func<Expression, Expression, BinaryExpression>;
    using Unary = Func<Expression, UnaryExpression>;
    using TypeUnary = Func<Expression, Type, UnaryExpression>;
    public static class Operator<T>
    {
        private static readonly ParameterExpression X = Expression.Parameter(typeof(T), "x");
        private static readonly ParameterExpression Y = Expression.Parameter(typeof(T), "y");

        public static readonly Func<T, T, T> Add = Lambda(Expression.Add);
        public static readonly Func<T, T, T> Subtract = Lambda(Expression.Subtract);
        public static readonly Func<T, T, T> Multiply = Lambda(Expression.Multiply);
        public static readonly Func<T, T, T> Divide = Lambda(Expression.Divide);
        public static readonly Func<T, T> Plus = Lambda(Expression.UnaryPlus);
        public static readonly Func<T, T> Negate = Lambda(Expression.Negate);
        public static readonly Func<T, float> ToFloat = Convert<float>(Expression.Convert);
        public static readonly Func<T, double> ToDouble = Convert<double>(Expression.Convert);
        public static readonly Func<T, int> ToInt = Convert<int>(Expression.Convert);
        public static readonly Func<T, T,T> LeftShift = Lambda(Expression.LeftShift);
        public static readonly Func<T, T, T> RightShift = Lambda(Expression.RightShift);
        public static readonly Func<T, T, T> Or = Lambda(Expression.Or);
        public static readonly Func<T, T, T> And = Lambda(Expression.And);
        public static readonly Func<T, T, T> ExclusiveOr = Lambda(Expression.ExclusiveOr);
        public static Func<T, T, T> Lambda(Binary op)
        {
            return Expression.Lambda<Func<T, T, T>>(op(X, Y), X, Y).Compile();
        }

        public static Func<T, T> Lambda(Unary op)
        {
            return Expression.Lambda<Func<T, T>>(op(X), X).Compile();
        }

        public static Func<T, TU> Convert<TU>(TypeUnary op )
        {
            return Expression.Lambda<Func<T, TU>>(op(X, typeof(TU)), X).Compile();
        }
    }
}