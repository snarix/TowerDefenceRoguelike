namespace _Source.Gameplay.UI
{
    public class ScaleOnPointerWithSibling : ScaleOnPointerAnimation
    {
        protected override bool IsSetAsFirstSibling() => true;
        protected override bool IsSetAsLastSibling() => true;
    }
}