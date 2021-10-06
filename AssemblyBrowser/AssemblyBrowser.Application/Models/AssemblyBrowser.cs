﻿using AssemblyBrowser.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser.Application.Models
{
    public class AssemblyBrowser : IAssemblyBrowser
    {
        private ICachingService _cachingService;
        private IHierarchyGenerator _hierarchyGenerator;

        public AssemblyBrowser(ICachingService cachingService, IHierarchyGenerator hierarchyGenerator)
        {
            _cachingService = cachingService;
            _hierarchyGenerator = hierarchyGenerator;
        }

        public Assembly[] GetApplicationAssemblies()
        {
            return (Assembly[])_cachingService.GetOrSearch(
                "application_assemblies",
                () => AppDomain.CurrentDomain.GetAssemblies());
        }

        public Type[] GetAssemblyTypes(Assembly assembly)
        {
            return (Type[])_cachingService.GetOrSearch(
                assembly.FullName,
                () => assembly.GetTypes());
        }

        public MemberInfo[] GetTypeMembersInfo(Type type)
        {
            return ((MemberInfo[])_cachingService.GetOrSearch(
                type.FullName,
                () => type.GetMembers()));
        }

        public Tuple<int, Type>[] GetTypeHierarchy(Type type)
        {
            return (Tuple<int, Type>[])_cachingService.GetOrSearch(
                type.FullName,
                () => _hierarchyGenerator.GetTypeHierarchy(type));
        }   
    }
}