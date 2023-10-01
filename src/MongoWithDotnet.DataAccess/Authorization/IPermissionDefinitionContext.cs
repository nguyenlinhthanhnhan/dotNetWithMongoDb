﻿using MongoWithDotnet.Core.Authorization;

namespace MongoWithDotnet.DataAccess.Authorization;

/// <summary>
/// This context is used on <see cref="AuthorizationProvider.SetPermissions"/> method.
/// </summary>
public interface IPermissionDefinitionContext
{
    /// <summary>
    /// Creates a new permission under this group.
    /// </summary>
    /// <param name="name">Unique name of the permission</param>
    /// <param name="displayName">Display name of the permission</param>
    /// <param name="description">A brief description for this permission</param>
    /// <param name="properties">Custom Properties. Use this to add your own properties to permission.</param>
    /// <returns>New created permission</returns>
    Permission CreatePermission(
        string name,
        string? displayName = null,
        string? description = null,
        Dictionary<string, object>? properties = null
    );

    /// <summary>
    /// Gets a permission with given name or null if can not find.
    /// </summary>
    /// <param name="name">Unique name of the permission</param>
    /// <returns>Permission object or null</returns>
    Permission? GetPermissionOrNull(string name);

    /// <summary>
    /// Remove permission with given name
    /// </summary>
    /// <param name="name"></param>
    void RemovePermission(string name);
}