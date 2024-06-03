using System;

public class Hopital
{
    private static Hopital instance = null;

    public static Hopital Instance
    {
        get
        {
            if (instance == null)
                instance = new Hopital();
            return instance;
        }
    }

    public Queue<Patient> FileAttente { get; private set; }
    public DAOPatient DaoPatient { get; private set; }
    public DAOVisite DaoVisite { get; private set; }

    private Hopital()
    {
        FileAttente = new Queue<Patient>();
        DaoPatient = new DAOPatient(CONNEXION_INFO);
        DaoVisite = new DAOVisite(CONNEXION_INFO);
    }
}
