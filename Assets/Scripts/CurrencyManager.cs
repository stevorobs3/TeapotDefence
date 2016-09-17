using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    private int _balance = 0;

    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = _balance.ToString();
    }


    public int Balance
    {
        get
        {
            return _balance;
        }
    }   


    public void Deposit(int amount)
    {
        _balance += amount;
    }

    public bool Spend(int amount)
    {
        _balance -= amount;
        var transactionSuccess = _balance >= 0;
        _balance = Mathf.Max(0, _balance);
        return transactionSuccess;
    }
}
