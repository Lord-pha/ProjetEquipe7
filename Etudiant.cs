// Dans Etudiant.cs (à séparer idéalement)
public class Etudiant
{
    public int Id { get; set; }
    public string Matricule { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateNaissance { get; set; }
    public DateTime DateInscription { get; set; }

    // Implémentation du code 'Ajouter', 'Modifier', 'Supprimer', 'Consulter'
    public void Ajouter()
    {
        // Votre code existant + Améliorations futures (ex: validation des données)
        if (!GestionAuthentification.EstAutorise("Admin")) { Console.WriteLine("Accès refusé..."); return; }
        Console.WriteLine($"\n[ETUDIANT] Ajout de l'étudiant {Nom} {Prenom} ({Matricule}).");
    }
    
    // Le membre devrait améliorer cette méthode :
    public void Modifier()
    {
        Console.WriteLine($"[ETUDIANT] Modification complète des informations de {Nom} {Prenom}.");
    }

    // Le membre devrait s'assurer que cette méthode fonctionne :
    public void Supprimer()
    {
        if (!GestionAuthentification.EstAutorise("Admin")) { Console.WriteLine("Accès refusé..."); return; }
        Console.WriteLine($"[ETUDIANT] Suppression de l'étudiant {Nom} {Prenom}.");
    }
    
    // ... et toute autre méthode CRUD/Affichage nécessaire.
}