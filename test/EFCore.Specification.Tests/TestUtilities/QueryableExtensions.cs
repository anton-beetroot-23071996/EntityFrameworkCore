// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public static class QueryableExtensions
    {
        public static List<TSource> ToList<TSource>(this System.Collections.IEnumerable source)
            => source.OfType<TSource>().ToList();

        public static async Task<List<TSource>> ToListAsync<TSource>(this IQueryable source, CancellationToken cancellationToken = default)
        {
            var list = new List<TSource>();

            await using (var e = ((IQueryable<TSource>)source).AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken))
            {
                while (await e.MoveNextAsync().ConfigureAwait(false))
                {
                    list.Add(e.Current);
                }
            }

            return list;
        }
    }
}
