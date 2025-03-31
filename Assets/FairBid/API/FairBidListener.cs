//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//

namespace Fyber
{
    /// <summary>
    /// An Interface that contains events related to network start status triggered by a <see cref="FairBid" />
    /// </summary>
    public interface FairBidListener
    {
        /// <summary>
        /// Called when the mediated network is started.
        /// </summary>
        /// <param name="network">Network name as defined in <see cref="MediatedNetwork">.</param>
        void OnNetworkStarted(MediatedNetwork network);

        /// <summary>
        /// Called when the network failed to start.
        /// </summary>
        /// <param name="network">Network name as defined in <see cref="MediatedNetwork">.</param>
        /// <param name="error">Error.</param>
        void OnNetworkFailedToStart(MediatedNetwork network, string error);

        /// <summary>
        /// Called when the mediation is started.
        /// </summary>
        void OnMediationStarted();

        /// <summary>
        /// Called when the network failed to start.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <param name="error">The error code.</param>
        void OnMediationFailedToStart(string error, string code);
    }
}