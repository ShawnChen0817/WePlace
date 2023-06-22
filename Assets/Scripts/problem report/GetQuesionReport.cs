using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetQuesionReport : MonoBehaviour
{
    
    public GameObject Username;
    public GameObject Email;
    public GameObject Problem;

    private string Name;
    private string Email_Address;
    private string Problem_report;
   
    [SerializeField] private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdt-TQLwspFO3uM-J_t-Dsbg8kwKZsXK_Gh9QL3mE-L7SWoow/formResponse";

    IEnumerator Post(string name, string email, string problem)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1345986113",name);
        form.AddField("entry.1601116067",email);
        form.AddField("entry.1483498123",problem);

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }
    public void Submit()
    {
        Name = Username.GetComponent<InputField>().text;
        Email_Address = Email.GetComponent<InputField>().text;
        Problem_report = Problem.GetComponent<InputField>().text;
        Debug.Log("傳送成功");
        StartCoroutine(Post(Name, Email_Address, Problem_report));
        
    }
   
}
