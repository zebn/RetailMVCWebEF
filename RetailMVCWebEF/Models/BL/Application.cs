using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class Application : System.Web.HttpApplication
    {
        private const string ActiveSessionKey = "__ActiveSession__";

        private IDictionary<string, OnlineUser> ActiveSession
        {
            get
            {
                if (Application[ActiveSessionKey] == null)
                    Application[ActiveSessionKey] = new Dictionary<string, OnlineUser>();

                return Application[ActiveSessionKey] as IDictionary<string, OnlineUser>;
            }
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
            //if (ActiveSession != null && !ActiveSession.Keys.Contains(Session.SessionID))
            //    try { ActiveSession.Add(Session.SessionID, new OnlineUser()); }
            //    catch (Exception)
            //    {
            //        // ignored
            //    }
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
            if (ActiveSession != null && ActiveSession.Keys.Contains(Session.SessionID))
                try { ActiveSession.Remove(Session.SessionID); }
                catch (Exception)
                {
                    // ignored
                }
        }

        public int OnlineSessionCount(bool? login)
        {
            return ActiveSession?.Count(s => login == null || s.Value.Login == login) ?? 0;
        }

        public OnlineUser OnlineSession(string sessionId)
        {
            return ActiveSession != null && ActiveSession.Keys.Contains(sessionId) ? ActiveSession[sessionId] : null;
        }

        public void RegisterOnlineUser(string sessionId, string userId, string user, string username, string entidad, string userIp)
        {
            var _user = OnlineSession(sessionId);
            bool exist = _user != null;

            if (!exist)
                _user = new OnlineUser();

            _user.User = user;
            _user.Login = true;
            _user.UserIP = userIp;
            _user.UserID = userId;
            _user.Username = username;
            _user.Entidad = entidad;
            _user.IsAuthenticated = true;

            if (exist && ActiveSession != null)
                ActiveSession[sessionId] = _user;
            else if (ActiveSession != null && !exist)
                try { ActiveSession.Add(sessionId, _user); } catch (Exception) { }
        }

        public OnlineUser[] OnlineSessions(bool? Login)
        {
            return ActiveSession.Where(s => Login == null || s.Value.Login == Login)
                   .Select(s => s.Value).ToArray();
        }

        public void SetOfflineUser(string sessionId)
        {
            var user = OnlineSession(sessionId);

            if (user != null && user.Login)
            {
                user.Login = false;
                ActiveSession[sessionId] = user;
            }
        }
    }

    #region Classes

    public sealed class OnlineUser
    {
        public string UserID { get; set; }
        public string User { get; set; }
        public string Entidad { get; set; }
        public string Username { get; set; }
        public DateTime SessionStart { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserIP { get; set; }
        public bool Login { get; set; }

        public string OnlineTime => $"{(int)DateTime.Now.Subtract(SessionStart).TotalMinutes} min.";

        public OnlineUser()
        {
            Login = false;
            IsAuthenticated = false;
            SessionStart = DateTime.Now;
        }
    }
    #endregion
}