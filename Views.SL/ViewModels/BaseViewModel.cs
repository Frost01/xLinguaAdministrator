using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Views.SL.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetPropertyValue<T>(ref T oldValue, T newValue, Expression<Func<T>> expression)
        {
            if ((Equals(oldValue, default(T)) && Equals(newValue, default(T)))
                || Equals(oldValue, newValue))
            {
                return;
            }

            var propertyName = GetPropertyName(expression);

            oldValue = newValue;

            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Returns the string name of a property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns>The name of the property as a <see cref="System.String"/></returns>
        private static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException("Value must be a lamda expression", "expression");
            }

            if (!(expression.Body is MemberExpression))
            {
                throw new ArgumentException("The body of the expression must be a memberref", "expression");
            }

            var body = (MemberExpression)expression.Body;

            return body.Member.Name;
        }
    }

}
