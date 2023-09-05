namespace FallObject
{
    public class FallObject
    {
        public FallObjectView View => _view;
        public FallObjectModel Model => _model;

        private FallObjectView _view;
        private FallObjectModel _model;

        public FallObject(
            FallObjectView view, 
            FallObjectModel model)
        {
            _view = view;
            _model = model;
        }
    }
}