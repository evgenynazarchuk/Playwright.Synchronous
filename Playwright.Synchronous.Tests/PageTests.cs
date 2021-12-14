using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Playwright.MSTest;

namespace Playwright.Synchronous.Tests
{
    [TestClass]
    public class PageTests : PageTest
    {
        [TestMethod]
        public void WhenGetAttributeAfterTimeout()
        {
            Page.SetContent(@"
<html>
<head>
	<script defer>
	setTimeout(() => {
	let elem = document.querySelector('#app')
	elem.innerHTML += '<input disabled/>'
	}, 5000)
	</script>
</head>
<body>
	<div id = 'app'>
	</div>
</body>
</html>
");

            var value = Page.GetAttribute("//input", "disabled");
            Assert.AreEqual("", value);

        }

        [TestMethod]
        public void WhenGetAttributeWithValue()
        {
            Page.SetContent(@"
<html>
<head>
</head>
<body>
	<div id = 'app'>
<input type='text'>
	</div>
</body>
</html>
");

            var value = Page.GetAttribute("//input", "type");
            Assert.AreEqual("text", value);

        }

        [TestMethod]
        public void WhenGetAttributeNotExistAttribute()
        {
            Page.SetContent(@"
<html>
<head>
</head>
<body>
	<div id = 'app'>
<input type='text'>
	</div>
</body>
</html>
");

            var value = Page.GetAttribute("//input", "class");
            Assert.IsNull(value);

        }
    }
}