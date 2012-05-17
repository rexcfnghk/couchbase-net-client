﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Couchbase.Configuration;
using Enyim.Caching.Memcached;

namespace Couchbase.Tests
{
	[TestFixture]
	public class ConfigHelperTests : CouchbaseClientTestsBase
	{
		[Test]
		public void Client_Operations_Succeed_When_Bootstrapping_To_Pools_Root_Uri()
		{
			var config = new CouchbaseClientConfiguration();
			config.Urls.Add(new Uri("http://localhost:8091/pools"));
			config.Bucket = "default";

			var client = new CouchbaseClient(config);

			string key = GetUniqueKey(), value = GetRandomString();
			var storeResult = client.ExecuteStore(StoreMode.Add, key, value);
			StoreAssertPass(storeResult);

			var getResult = client.ExecuteGet(key);
			GetAssertPass(getResult, value);

		}

		[Test]
		public void Client_Operations_Fail_When_Bootstrapping_To_Pools_Default_Root_Uri()
		{
			var config = new CouchbaseClientConfiguration();
			config.Urls.Add(new Uri("http://localhost:8091/pools/default"));
			config.Bucket = "default";

			var client = new CouchbaseClient(config);

			string key = GetUniqueKey(), value = GetRandomString();
			var storeResult = client.ExecuteStore(StoreMode.Add, key, value);
			Assert.That(storeResult.Success, Is.Not.True, "Success was true");
			Assert.That(storeResult.Message, Is.Not.Empty, "Message was empty");

			var getResult = client.ExecuteGet(key);
			GetAssertFail(getResult);

		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2012 Couchbase, Inc.
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion
