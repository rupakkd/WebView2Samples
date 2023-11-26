// Copyright (C) Microsoft Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebView2WpfBrowser
{
    // BridgeAddRemoteObject is a .NET object that implements IDispatch and works with AddRemoteObject.
    // See the AddRemoteObjectCmdExecute method that demonstrates how to use it from JavaScript.
#pragma warning disable CS0618
    // The.NET version of CoreWebView2.AddHostObjectToScript currently relies on the host object
    // implementing IDispatch and so uses the deprecated ClassInterfaceType.AutoDual feature of.NET.
    // This may change in the future, please see https://github.com/MicrosoftEdge/WebView2Feedback/issues/517  for more information
    [ClassInterface(ClassInterfaceType.AutoDual)]
#pragma warning restore CS0618
    [ComVisible(true)]
    public class AnotherRemoteObject
    {

        // Sample property.
        public string Prop { get; set; } = "AnotherRemoteObject.Prop";
    }

#pragma warning disable CS0618
    [ClassInterface(ClassInterfaceType.AutoDual)]
#pragma warning restore CS0618
    [ComVisible(true)]
    public class BridgeAddRemoteObject
    {
        // Sample function that takes a parameter.
        public string Func(string param)
        {
            return "BridgeAddRemoteObject.Func(" + param + ")";
        }

        public async Task<string> FuncAsync(int msDelay)
        {
            if (msDelay > 0)
            {
                await Task.Delay(msDelay);
            }

            return $"BridgeAddRemoteObject.FuncAsync({msDelay})";
        }

        public async Task FuncAsyncTaskVoid(int msDelay)
        {
            await Task.Delay(msDelay);
        }

        // Sample function that takes no parameters.
        public string Func2()
        {
            return "BridgeAddRemoteObject.Func2()";
        }

        public async Task<string> Func2Async()
        {
            await Task.Delay(500);
            return "BridgeAddRemoteObject.Func2Async()";
        }

        public async Task Func2AsyncTaskVoid()
        {
            await Task.Delay(500);
        }

        // Get type of an object.
        public string GetObjectType(object obj)
        {
            return obj.GetType().Name;
        }

        public async Task<string> GetObjectTypeAsync(object obj)
        {
            await Task.Delay(500);
            return GetObjectType(obj);
        }

        // Sample property.
        public string Prop { get; set; } = "BridgeAddRemoteObject.Prop";

        public DateTime DateProp { get; set; } = DateTime.UtcNow;

        public AnotherRemoteObject AnotherObject { get; set; } = new AnotherRemoteObject();

        // Sample indexed property.
        [System.Runtime.CompilerServices.IndexerName("Items")]
        public string this[int index]
        {
            get { return _dictionary[index]; }
            set { _dictionary[index] = value; }
        }
        private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>();

        public void InvokeEvent()
        {
            TestEvent0?.Invoke();
            TestEvent1?.Invoke("param1");
            TestEvent2?.Invoke("param1", DateTime.UtcNow);
        }
#pragma warning disable CS0067
#pragma warning disable IDE1006 // Naming Styles
        public delegate void TestEvent0Delegate();
        public delegate void TestEvent1Delegate(String param1);
        public delegate void TestEvent2Delegate(String param1, DateTime param2);
#pragma warning restore IDE1006 // Naming Styles
        public event TestEvent0Delegate TestEvent0;
        public event TestEvent1Delegate TestEvent1;
        public event TestEvent2Delegate TestEvent2;
#pragma warning restore CS0067
    }
}
