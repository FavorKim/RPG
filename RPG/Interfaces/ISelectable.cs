

namespace Managers.Selectable
{
    interface ISelectable
    {
        bool IsSelected { get; set; }
        string Name { get; set; }
        void Use();
        void ShowNum();
    }
}
