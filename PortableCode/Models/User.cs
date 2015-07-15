﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PortableCode.BusinessLayer.Contracts;

namespace PortableCode.Models
{
    public class User : BusinessEntityBase
    {

        [JsonProperty(PropertyName = "Id")]
        public string UserId { get; set; }

        [JsonProperty/*(PropertyName = "Password")*/]
        public string Password { get; set; }

        [JsonProperty/*(PropertyName = "TeritorryId")*/]
        public int TeritorryId { get; set; }

        public User()
        {
            UserId = string.Empty;
            Password = string.Empty;
            TeritorryId = int.MinValue;
        }

        public User(User anUser)
        {
          SyncProperties(anUser);
        }

        public void SyncProperties(User anUser)
        {
          this.UserId = anUser.UserId;
          this.Password = anUser.Password;
          this.TeritorryId = anUser.TeritorryId;
        }

        public bool IsDirty { get; set; }

    }
}