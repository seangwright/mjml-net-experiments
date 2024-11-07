using Mjml.Net;
using Tests.Internal;

var mjmlRenderer = new MjmlRenderer();

string buttonText = """
        <mj-raw>
            <!-- MJML-COMPONENT-START -->
        </mj-raw>
        <mj-section data-mjml-component-start>
            <mj-column>
                <mj-button font-family="Helvetica" background-color="#f45e43" color="white">
                Don't click me!
                </mj-button>
            </mj-column>
        </mj-section data-mjml-component-end>
        <mj-raw>
            <!-- MJML-COMPONENT-END -->
        </mj-raw>
""";

string sectionText = """
<mj-column width="35%">
    <mj-raw>
        <WidgetZone />
    </mj-raw>
</mj-column>
<mj-column width="65%">
    <mj-raw>
        <WidgetZone />
    </mj-raw>
</mj-column>
""";

string bodyText = """
<mjml>
    <mj-head>
        <mj-title>Hello World Example</mj-title>
    </mj-head>
    <mj-body>
        <mj-section>
            <mj-column>
                <mj-text>
                    Hello World!
                </mj-text>
            </mj-column>
            <mj-include path="button.mjml" />
        </mj-section>
        <mj-include path="section.mjml" />
    </mj-body>
</mjml>
""";

var options = new MjmlOptions
{
    Beautify = false,
    KeepComments = true,
    FileLoader = () => new InMemoryFileLoader(new Dictionary<string, string>()
    {
        { "button.mjml", buttonText },
        { "section.mjml", sectionText }
    })
};

var (buttonHTML, buttonErrors) = mjmlRenderer.Render(buttonText, options);
var (sectionHTML, sectionErrors) = mjmlRenderer.Render(sectionText, options);
var (fullEmailHTML, errors) = mjmlRenderer.Render(bodyText, options);

Console.WriteLine("BUTTON\r\n");
Console.WriteLine(buttonHTML);
Console.WriteLine("\r\n");
if (buttonErrors.Any())
{
    Console.WriteLine(string.Join("\r\n", buttonErrors.Select(e => e.Error)));
    Console.WriteLine("\r\n");
}

Console.WriteLine("SECTION\r\n");
Console.WriteLine(sectionHTML);
Console.WriteLine("\r\n");
if (sectionErrors.Any())
{
    Console.WriteLine(string.Join("\r\n", sectionErrors.Select(e => e.Error)));
    Console.WriteLine("\r\n");
}

Console.WriteLine("FULL EMAIL\r\n");
Console.WriteLine(fullEmailHTML);
Console.WriteLine("\r\n");
if (errors.Any())
{
    Console.WriteLine(string.Join("\r\n", errors.Select(e => e.Error)));
    Console.WriteLine("\r\n");
}

