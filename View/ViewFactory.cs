using System;
using System.Collections.Generic;
using Entities;

namespace View
{
    public class ViewFactory : IViewFactory
    {
        private readonly Dictionary<string, Type> _viewTable = new Dictionary<string, Type>();

        public ViewFactory()
        {
            _viewTable.Add("EditView", typeof(EditView));
            _viewTable.Add("ImportView", typeof(ImportView));
        }
        public IView Create(string viewName)
        {
            if(string.IsNullOrWhiteSpace(viewName)) throw new ArgumentNullException(nameof(viewName), "Cannot be null or empty");
            if (!_viewTable.ContainsKey(viewName)) throw  new InvalidOperationException($"There is't any object defined for {viewName}");
            return (IView)Activator.CreateInstance(_viewTable[viewName]);
        }
    }
}