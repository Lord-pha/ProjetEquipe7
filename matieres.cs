// Dans Matiere.cs(à séparer idéalement)
public class Matiere
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Code { get; set; }
    public int Credits { get; set; }

    // Le membre devrait s'assurer que cette méthode est complète :
    public void Creer()
    {
        Console.WriteLine($"[MATIERE] Création de la matière {Nom} ({Code}).");
    }

    // Le membre devrait implémenter la logique de modification :
    public void Modifier()
    {
        Console.WriteLine($"[MATIERE] Modification détaillée des informations de la matière {Nom}.");
    }

    // Le membre devrait implémenter la logique de suppression :
    public void Supprimer()
    {
        Console.WriteLine($"[MATIERE] Suppression de la matière {Nom}.");
    }
    // Le membre devrait détailler la répartition (si c'est leur tâche) :
    public void RepartirEnseignants()
    {
        if (!GestionAuthentification.EstAutorise("Admin")) { Console.WriteLine("❌ Accès refusé..."); return; }
        // TODO: Implémenter la logique d'assignation des enseignants à la matière
        Console.WriteLine($"[MATIERE] Répartition des enseignants pour {Nom} effectuée.");
    }
}

