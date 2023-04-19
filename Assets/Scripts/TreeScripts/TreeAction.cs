using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeAction : ITreeNode
{
    //un delegate encapsula un método y lo usas cuando queres
    //EJ: public delegate bool MyDelegate(int a, float b);

    //el ACTION puede recibir bocha de parámetros y no recibir
    //nada, termina siendo un void.

    //el func si o si tiene que devolver algo. le paso todo y el último
    //es lo que devuelve.

    Action myAction;

    public TreeAction(Action action)
    {
        //le pasamos un método a la acción.
        myAction = action;
    }

    public void Execute()
    {
        //en excecute ejecutamos el método.
        if (myAction!= null)
        {
            myAction();
        }
    }
}
