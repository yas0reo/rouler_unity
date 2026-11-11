using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class oscProcess : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmitter;
    public Player Player;


    public int betaInMin = -40;
    public int betaInMax = 40;

    public float betaOutMin = 0.0f;
    public float betaOutMax = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/beta", TraiterMessageBeta);

        oscReceiver.Bind("/alpha", TraiterMessageAlpha);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void TraiterMessageBeta(OSCMessage message)
    {
        // Traiter un entier et l’appliquer à une propriété (flux continu)

        // Récupérer la valeur du premier argument en taant qu’entier :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // Ajustement de la valeur
        float ajustee = (
            ((float)valeur - betaInMin) / (betaInMax - betaInMin) * (betaOutMax - betaOutMin) + betaOutMin
        );

        // DESTINATION
        Player.AddTorque(ajustee);
    }

    void TraiterMessageAlpha(OSCMessage message)
    {

        // Traiter un entier booléen (évènement discret)

        // Récupérer la valeur du premier argument en taant qu’entier :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // Traiter la valeur
        if (valeur == 1)
        {
           Player.Jump();
        }
        else
        {
            // Si nécéssaire, appeler une fonction ici
        }

    }


}
