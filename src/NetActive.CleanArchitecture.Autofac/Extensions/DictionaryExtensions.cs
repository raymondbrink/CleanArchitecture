namespace NetActive.CleanArchitecture.Autofac.Extensions;

using System;
using System.Collections.Generic;

using global::Autofac;
using global::Autofac.Core;

public static class DictionaryExtensions
{
    /// <summary>
    /// Gets the specified parameter from the dictionary.
    /// </summary>
    /// <param name="registrationParams">Dictionary of parameters to be used during registration.</param>
    /// <param name="paramName">Name of the parameter to return.</param>
    /// <param name="canBeNull">if set to <c>true</c> setting can be null.</param>
    /// <param name="defaultValue">Optional default value to be used, if the parameter value is NULL or the parameter is not configured.</param>
    /// <returns>Parameter to be used during registration in Autofac.</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static Parameter GetParameter(
        this IDictionary<string, object> registrationParams,
        string paramName,
        bool canBeNull = true,
        object defaultValue = null)
    {
        var paramValue = registrationParams.GetParameterValue(paramName) ?? defaultValue;

        assertParamValue(paramName, paramValue, canBeNull);

        return new NamedParameter(paramName, paramValue);
    }

    /// <summary>
    /// Gets the specified parameter value from the dictionary.
    /// </summary>
    /// <param name="registrationParams">Dictionary of parameters to be used during registration.</param>
    /// <param name="paramName">Name of the parameter to return.</param>
    /// <param name="canBeNull">if set to <c>true</c> setting can be null.</param>
    /// <returns>Parameter value object.</returns>
    public static object GetParameterValue(
        this IDictionary<string, object> registrationParams,
        string paramName,
        bool canBeNull = true)
    {
        object paramValue = null;
        registrationParams?.TryGetValue(paramName, out paramValue);

        assertParamValue(paramName, paramValue, canBeNull);

        return paramValue;
    }

    private static void assertParamValue(string paramName, object paramValue, bool canBeNull)
    {
        if (!canBeNull && (paramValue == null || string.IsNullOrWhiteSpace(paramValue.ToString())))
        {
            throw new ArgumentNullException(paramName, $"Parameter '{paramName}' can't be null or empty.");
        }
    }
}