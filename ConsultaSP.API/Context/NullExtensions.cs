using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Context
{
    public static class NullExtensions
    {
        public static object IsNullValue(string value) => string.IsNullOrEmpty(value) ? (object)DBNull.Value : (object)value;

        public static object IsNullValue(int? value) => !value.HasValue ? (object)DBNull.Value : (object)value.Value;

        public static object IsNullValue(Decimal? value) => !value.HasValue ? (object)DBNull.Value : (object)value.Value;

        public static object IsNullValue(long? value) => !value.HasValue ? (object)DBNull.Value : (object)value.Value;

        public static object IsNullValue(bool? value) => !value.HasValue ? (object)DBNull.Value : (object)value.Value;

        public static object IsNullValue(DateTime? value) => !value.HasValue ? (object)DBNull.Value : (object)value.Value;
    }
}
