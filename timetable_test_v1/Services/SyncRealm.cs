using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetable_test_v1.Services
{
    public partial class SyncRealm
    {

        public static Realm GetRealms()
        {
            FlexibleSyncConfiguration config = new FlexibleSyncConfiguration(App.RealmApp.CurrentUser);
            Realm realm = Realm.GetInstance(config);

            return realm;
        }
    }
}
