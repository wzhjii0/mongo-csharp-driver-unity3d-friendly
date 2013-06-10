﻿/* Copyright 2010-2013 10gen Inc.
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
using System.Threading;

namespace MongoDB.Driver.Core.Connections
{
    /// <summary>
    /// Manages one or more instances of a <see cref="IServer"/>.
    /// </summary>
    public interface ICluster : IDisposable
    {
        // public properties
        /// <summary>
        /// Gets the description.
        /// </summary>
        ClusterDescription Description { get; }

        /// <summary>
        /// Initializes the cluster.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Selects a server using the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
        /// <returns>A server.</returns>
        IServer SelectServer(IServerSelector selector, TimeSpan timeout, CancellationToken cancellationToken);
    }

    public static class IClusterExtensions
    {
        /// <summary>
        /// Selects a server using the specified selector.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <param name="serverSelector">The server selector.</param>
        /// <returns>A server.</returns>
        public static IServer SelectServer(this ICluster @this, IServerSelector serverSelector)
        {
            return @this.SelectServer(serverSelector, TimeSpan.FromMilliseconds(Timeout.Infinite), CancellationToken.None);
        }

        /// <summary>
        /// Selects a server using the specified selector.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
        /// <returns>A server.</returns>
        public static IServer SelectServer(this ICluster @this, IServerSelector serverSelector, CancellationToken cancellationToken)
        {
            return @this.SelectServer(serverSelector, TimeSpan.FromMilliseconds(Timeout.Infinite), cancellationToken);
        }

        /// <summary>
        /// Selects a server using the specified selector.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
        /// <returns>A server.</returns>
        public static IServer SelectServer(this ICluster @this, IServerSelector serverSelector, TimeSpan timeout)
        {
            return @this.SelectServer(serverSelector, timeout, CancellationToken.None);
        }
    }
}