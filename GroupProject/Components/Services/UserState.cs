using System;

namespace GroupProject.Components.Services
{
    public class UserState
    {
        public string? Username { get; private set; }
        public string? UserRole { get; private set; }

        public event Action? OnChange;

        public void SetUser(string username, string role)
        {
            Username = username;
            UserRole = role;
            NotifyStateChanged();
        }

        public void ClearUser()
        {
            Username = null;
            UserRole = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
} 