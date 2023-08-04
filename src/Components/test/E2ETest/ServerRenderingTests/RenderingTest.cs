// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Components.TestServer.RazorComponents;
using Microsoft.AspNetCore.Components.E2ETest.Infrastructure;
using Microsoft.AspNetCore.Components.E2ETest.Infrastructure.ServerFixtures;
using Microsoft.AspNetCore.E2ETesting;
using OpenQA.Selenium;
using TestServer;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Components.E2ETests.ServerRenderingTests;

public class RenderingTest : ServerTestBase<BasicTestAppServerSiteFixture<RazorComponentEndpointsStartup<App>>>
{
    public RenderingTest(
        BrowserFixture browserFixture,
        BasicTestAppServerSiteFixture<RazorComponentEndpointsStartup<App>> serverFixture,
        ITestOutputHelper output)
        : base(browserFixture, serverFixture, output)
    {
    }

    public override Task InitializeAsync()
        => InitializeAsync(BrowserFixture.StreamingContext);

    [Fact]
    public void CanRenderLargeComponentsWithServerRenderMode()
    {
        Navigate($"{ServerPathBase}/large-html-server");
        var webAssemblyPrerender = Browser.FindElement(By.Id("webassembly-prerender"));
        var serverPrerender = Browser.FindElement(By.Id("server-prerender"));
        var serverNoPrerender = Browser.FindElement(By.Id("server-prerender"));
        var result = new string('*', 50000);

        Assert.Equal(result, webAssemblyPrerender.Text);
        Assert.Equal(result, serverPrerender.Text);
        Assert.Equal(result, serverNoPrerender.Text);
    }
}
