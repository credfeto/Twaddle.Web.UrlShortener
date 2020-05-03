﻿using System;
using System.Diagnostics.Contracts;
using Credfeto.UrlShortener.Shorteners;

namespace Credfeto.UrlShortener
{
    /// <summary>
    ///     Factory for creating Url Shorteners.
    /// </summary>
    public static class ShortenerFactory
    {
        /// <summary>
        ///     Creates an instance of the specified publisher.
        /// </summary>
        /// <param name="type">
        ///     The type of the publisher to create.
        /// </param>
        /// <returns>
        ///     The requested publisher if a matching one can be found; otherwise, <see langword="null" />.
        /// </returns>
        public static IUrlShortener Create(string type)
        {
            Contract.Requires(!string.IsNullOrEmpty(type));
            Contract.Ensures(Contract.Result<IUrlShortener>() != null);

            if (StringComparer.InvariantCultureIgnoreCase.Equals(type, "Bitly")) return new Bitly();

            if (StringComparer.InvariantCultureIgnoreCase.Equals(type, "Google")) return new Google();

            return new Generic(DoNotShorten);
        }


        private static Uri DoNotShorten(Uri fullUrl)
        {
            Contract.Requires(fullUrl != null);
            Contract.Ensures(Contract.Result<Uri>() != null);
            Contract.Ensures(Contract.Result<Uri>() == fullUrl);

            return fullUrl;
        }
    }
}