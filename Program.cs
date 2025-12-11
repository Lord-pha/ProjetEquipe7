using System.Collections.Generic;
using System.Linq;

namespace GestionBulletins
{

internal class Program
{
    private static List<Etudiant> EtudiantsDB = new List<Etudiant>();
    private static List<Matiere> MatieresDB = new List<Matiere>();
    private static List<Enseignant> EnseignantsDB = new List<Enseignant>();

    private static int NextEtudiantId = 1;
    private static int NextMatiereId = 101;
    private static int NextEnseignantId = 501;

    static void Main(string[] args)
    {


        
        Console.WriteLine("=================================================");
        Console.WriteLine("   PROJET SIMPLIFIÉ (CONSOLE)   ");
        Console.WriteLine("=================================================");

        // Initialisation des données pour le test
        MatieresDB.Add(new Matiere { Id = NextMatiereId++, Nom = "Mathématiques", Code = "MATH101", Credits = 5 });
        EtudiantsDB.Add(new Etudiant { Id = NextEtudiantId++, Nom = "Smith", Prenom = "Alice", Matricule = "E201", DateNaissance = new DateTime(2000, 5, 15), DateInscription = DateTime.Now });
        EnseignantsDB.Add(new Enseignant { Id = NextEnseignantId++, Nom = "Dupont", Prenom = "Marie", Email = "m.dupont@ecole.com", Specialite = "Info", EstResponsable = true });

        while (true)
        {
            AfficherMenuAuthentification();
            Console.Write("\nVotre choix : ");
            string choix = Console.ReadLine();

            if (choix == "3") break;

            if (TenterAuthentification(choix))
            {
                AfficherMenuPrincipal();
                GestionAuthentification.Logout();
            }
        }

        Console.WriteLine("\nProgramme terminé. Au revoir!");
    }

    // --- LOGIQUE D'AUTHENTIFICATION 

    private static void AfficherMenuAuthentification()
    {
        Console.WriteLine("\n--- AUTHENTIFICATION ---");
        Console.WriteLine("1. Admin (User: admin, Pass: mdpadmin)");
        Console.WriteLine("2. Enseignant (User: profmaths, Pass: mdpprof)");
        Console.WriteLine("3. Quitter");
    }

    private static bool TenterAuthentification(string choix)
    {
        string user = "", pass = "";

        switch (choix)
        {
            case "1": user = "admin"; pass = "mdpadmin"; break;
            case "2": user = "profmaths"; pass = "mdpprof"; break;
            default: Console.WriteLine("Choix invalide."); return false;
        }

        return GestionAuthentification.Login(user, pass);
    }

    // --- LOGIQUE DE MENU PRINCIPAL ---

    private static void AfficherMenuPrincipal()
    {
        var utilisateur = GestionAuthentification.GetUtilisateurConnecte();
        if (utilisateur == null) return;

        bool enCours = true;
        while (enCours)
        {
            Console.WriteLine($"\n--- MENU PRINCIPAL ({utilisateur.Role}) ---");
            Console.WriteLine("1. Gérer les Étudiants");
            Console.WriteLine("2. Gérer les Enseignants");
            Console.WriteLine("3. Gérer les Matières");
            Console.WriteLine("9. Se déconnecter");

            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1": MenuEtudiants(); break;
                case "2": MenuEnseignants(); break;
                case "3": MenuMatieres(); break;
                case "9": enCours = false; break;
                default: Console.WriteLine("Choix invalide."); break;
            }
        }
    }

    // --- SOUS-MENUS DE GESTION ---

    private static void MenuEtudiants()
    {
        Console.WriteLine("\n--- GESTION DES ÉTUDIANTS ---");
        Console.WriteLine("1. Ajouter un Étudiant");
        Console.WriteLine("2. Consulter tous les Étudiants");
        Console.WriteLine("9. Retour");
        Console.Write("Votre choix : ");
        string choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                if (GestionAuthentification.EstAutorise("Admin")) SaisirEtudiant();
                else Console.WriteLine("❌ Seul l'Admin peut ajouter des étudiants.");
                break;
            case "2":
                foreach (var e in EtudiantsDB) e.Consulter();
                break;
        }
    }

    private static void MenuEnseignants()
    {
        Console.WriteLine("\n--- GESTION DES ENSEIGNANTS ---");
        Console.WriteLine("1. Créer un Enseignant (CreerCompter)");
        Console.WriteLine("2. Coordonner l'Équipe (Test)");
        Console.WriteLine("9. Retour");
        Console.Write("Votre choix : ");
        string choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                if (GestionAuthentification.EstAutorise("Admin")) EnseignantsDB.First().CreerCompter();
                else Console.WriteLine("❌ Seul l'Admin peut créer des comptes enseignants.");
                break;
            case "2":
                EnseignantsDB.First().CoordonnerEquipe(); // Test sur le premier enseignant
                break;
        }
    }

    private static void MenuMatieres()
    {
        Console.WriteLine("\n--- GESTION DES MATIÈRES ---");
        Console.WriteLine("1. Créer une Matière");
        Console.WriteLine("2. Consulter toutes les Matières");
        Console.WriteLine("3. Répartir Enseignants");
        Console.WriteLine("9. Retour");
        Console.Write("Votre choix : ");
        string choix = Console.ReadLine();

        switch (choix)
        {
            case "1": SaisirMatiere(); break;
            case "2": foreach (var m in MatieresDB) m.Consulter(); break;
            case "3":
                if (GestionAuthentification.EstAutorise("Admin")) MatieresDB.First().RepartirEnseignants();
                else Console.WriteLine("❌ Seul l'Admin peut répartir les enseignants.");
                break;
        }
    }

    // --- FONCTIONNALITÉS DE SAISIE ---

    private static void SaisirEtudiant()
    {
        Console.WriteLine("\n--- SAISIE D'UN ÉTUDIANT ---");
        Console.Write("Nom : ");
        string nom = Console.ReadLine();
        Console.Write("Prénom : ");
        string prenom = Console.ReadLine();

        var nouvelEtudiant = new Etudiant
        {
            Id = NextEtudiantId++,
            Nom = nom,
            Prenom = prenom,
            Matricule = $"E{NextEtudiantId - 1}",
            DateNaissance = DateTime.Now,
            DateInscription = DateTime.Now
        };

        nouvelEtudiant.Ajouter(); // Affiche la confirmation
        EtudiantsDB.Add(nouvelEtudiant);
        Console.WriteLine($"✅ Étudiant ajouté : {nouvelEtudiant.ToString()}");
    }

    private static void SaisirMatiere()
    {
        Console.WriteLine("\n--- SAISIE D'UNE MATIÈRE ---");
        Console.Write("Nom de la matière : ");
        string nom = Console.ReadLine();
        Console.Write("Code (ex: INFO101) : ");
        string code = Console.ReadLine();

        var nouvelleMatiere = new Matiere
        {
            Id = NextMatiereId++,
            Nom = nom,
            Code = code,
            Credits = 5
        };

        nouvelleMatiere.Creer(); // Affiche la confirmation
        MatieresDB.Add(nouvelleMatiere);
        Console.WriteLine($"✅ Matière ajoutée : {nouvelleMatiere.ToString()}");
    }



} }