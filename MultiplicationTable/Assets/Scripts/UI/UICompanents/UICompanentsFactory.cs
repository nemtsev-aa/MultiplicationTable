using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UICompanentsFactory", menuName = "Factory/UICompanentsFactory")]
public class UICompanentsFactory : ScriptableObject {
    [SerializeField] private List<UICompanent> _companents;

    private UICompanentVisitor _visitor;

    private UICompanent Companent => _visitor.Companent;

    public T Get<T>(UICompanentConfig config, RectTransform parent) where T : UICompanent {
        _visitor = new UICompanentVisitor(_companents);
        _visitor.Visit(config);

        return (T)Instantiate(Companent, parent);
    }
}
