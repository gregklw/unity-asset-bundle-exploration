using UnityEngine;

[CreateAssetMenu(fileName = "NewBundleUrlContainer", menuName = "ScriptableObjects/Create Bundle URL Container")]
public class BundleUrl : ScriptableObject
{
    [SerializeField] private string _url;
    public string Url => _url;
}
