using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Database : MonoBehaviour
{
    // Database connection.
    [SerializeField] private string databaseUrl;
    private DatabaseReference reference;

    // Database structure.
    [SerializeField] private string itemsName;

    // Database elements.
    [SerializeField] private string userId;
    [SerializeField] private string username;
    [SerializeField] private string email;

    // Buttons for manipulation and query.
    [SerializeField] private Button writeNewUserButton;
    [SerializeField] private Button updateUsernameButton;
    [SerializeField] private Button updateEmailButton;
    [SerializeField] private Button deleteUserButton;
    [SerializeField] private Button getUsernameButton;
    [SerializeField] private Button getEmailButton;

    /// <summary>
    /// Connect with database.
    /// </summary>
    public void OpenConnection()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// Initializing.
    /// </summary>
    private void Start()
    {
        // Add listener to buttons.
        writeNewUserButton.onClick.AddListener(WriteUser);
        updateUsernameButton.onClick.AddListener(UpdateUsername);
        updateEmailButton.onClick.AddListener(UpdateEmail);
        deleteUserButton.onClick.AddListener(RemoveUser);
        getUsernameButton.onClick.AddListener(GetUsername);
        getEmailButton.onClick.AddListener(GetEmail);
    }

    /// <summary>
    /// Add new item.
    /// </summary>
    private void WriteUser()
    {
        // Add user to constructor.
        User user = new User(username, email);

        // Convert user constructor to json format.
        string json = JsonUtility.ToJson(user);

        // Write user in database.
        reference.Child(itemsName).Child(userId).SetRawJsonValueAsync(json);
    }

    /// <summary>
    /// Set value to username.
    /// </summary>
    private void UpdateUsername()
    {
        reference.Child(itemsName).Child(userId).Child("Username").SetValueAsync(username);
    }

    /// <summary>
    /// Set value to email.
    /// </summary>
    private void UpdateEmail()
    {
        reference.Child(itemsName).Child(userId).Child("Email").SetValueAsync(email);
    }

    /// <summary>
    /// Delete item.
    /// </summary>
    private void RemoveUser()
    {
        reference.Child(itemsName).Child(userId).RemoveValueAsync();
    }

    /// <summary>
    /// Get username element value.
    /// </summary>
    private void GetUsername()
    {
        // Current username output.
        FirebaseDatabase.DefaultInstance.GetReference(itemsName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                Debug.Log(snapshot.Child(userId).Child("Username").Value);
            }
        });
    }

    /// <summary>
    /// Get email element value.
    /// </summary>
    private void GetEmail()
    {
        // Current email output.
        FirebaseDatabase.DefaultInstance.GetReference(itemsName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                Debug.Log(snapshot.Child(userId).Child("Email").Value);
            }
        });
    }
}