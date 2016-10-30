/*
 * Object Extensions - Doing stuff to objects in .NET
 * Copyright (C) 2016  Simon Wendel
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace SimonWendel.ObjectExtensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class MemberAccessExtensions
    {
        public static TTarget SetProperty<TTarget, TProperty>(this TTarget target, Expression<Func<TTarget, TProperty>> selector, TProperty value)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var memberSelector = selector.Body as MemberExpression;
            if (memberSelector != null)
            {
                var property = memberSelector.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                    return target;
                }
            }

            throw new InvalidOperationException(message: "Not a property.");
        }
    }
}
