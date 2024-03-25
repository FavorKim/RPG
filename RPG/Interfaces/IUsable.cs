using Managers.Selectable;

namespace Usable
{
    interface IUsable : ISelectable
    {
        int Consume { get; set; }
        bool CanUse();
        void Use();
        abstract void Effect();
    }
}
