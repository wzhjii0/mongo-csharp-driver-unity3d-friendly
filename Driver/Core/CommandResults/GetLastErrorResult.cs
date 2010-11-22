﻿/* Copyright 2010 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using MongoDB.Bson;

namespace MongoDB.Driver {
    [Serializable]
    public class GetLastErrorResult : CommandResult {
        #region constructors
        public GetLastErrorResult() {
        }
        #endregion

        #region public properties
        public int DocumentsAffected {
            get { return this["n"].ToInt32(); }
        }

        public string LastErrorMessage {
            get {
                var err = this["err", null];
                return (err == null || err.IsBsonNull) ? null : err.ToString();
            }
        }

        public bool UpdatedExisting {
            get {
                var updatedExisting = this["updatedExisting", null];
                return (updatedExisting == null) ? false : updatedExisting.ToBoolean();
            }
        }
        #endregion
    }
}
