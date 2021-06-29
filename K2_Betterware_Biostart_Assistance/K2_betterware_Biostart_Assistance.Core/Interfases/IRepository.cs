using System;

namespace K2_betterware_Biostart_Assistance.Core.Interfases
{
    public interface IRepository
    {
        
        string Token_bio();
        string User_bio();
        string Event_search_bio();
        string Device_bio();
        String bio_event_search(string tk_bio, string jsonb);
        
    }

}