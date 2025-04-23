namespace _Source.Gameplay.UI
{
    public class ScaleOnPointerWithSibling : ScaleOnPointerAnimation
    {
        protected override bool CanSetAsFirstSibling() => true;
        protected override bool CanSetAsLastSibling() => true;
    }
}