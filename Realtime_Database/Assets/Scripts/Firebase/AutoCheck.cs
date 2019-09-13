using System;
using UnityEngine;

public class AutoCheck : MonoBehaviour
{
    // Database connection instance.
    [SerializeField] private Database databaseScript; 

    /// <summary>
    /// Check for updates.
    /// </summary>
    private void Awake()
    {
        // Check dependencies.
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            // Cache Status.
            var dependencyStatus = task.Result;

            // Firebase available?
            if (dependencyStatus == Firebase.DependencyStatus.Available) // Yes.
            {
                // Output successful.
                Debug.Log("Firebase available!");

                // ToDo: Set flag, Firebase is ready to use.
                databaseScript.OpenConnection();
            }
            else // No.
            {
                // Output error.
                Debug.LogError(String.Format("Could not resolve all Firebase dependencies: {0}!", dependencyStatus));

                // ToDo: Set flag, Firebase can't used.
            }
        });
    }
}