window.addCallyChangeListener = (calendarElement, dotNetHelper) => {
    calendarElement.addEventListener('change', async (e) => {
        // console.log("javascript is running")

        const button = document.getElementById('cally1');
        if (button) {
            const text = document.getElementById('cally1-date-text')
            text.innerText = e.target.value || 'Pick a date';

            const popover = document.getElementById('cally-popover1');
            popover.hidePopover();

            if (dotNetHelper) {
                await dotNetHelper.invokeMethodAsync('HandleDateSelected', e.target.value)
            }
        }

        // Optional: dispatch custom event or store value in dotNet object
        // You can also call back to C# here if needed
    });

    // get the right end of window for FAB anchor
    const container = document.querySelector('#my-main-layout');
    const fab = document.querySelector('#my-fab');

    // console.log('container: ' + container);
    // console.log('fab: ' + fab);

    if (!container || !fab) return;

    const rect = container.getBoundingClientRect();
    const right = window.innerWidth - rect.right;

    // console.log("addFabListener running");
    // console.log("right: " + parseInt(right, 10));

    return right.toString();
};

window.addFabListener = (dotNetHelper) => {
    window.addEventListener('resize', async () => {
        const container = document.querySelector('#my-main-layout');
        const fab = document.querySelector('#my-fab');

        if (!container || !fab) return;

        const rect = container.getBoundingClientRect();
        const right = window.innerWidth - rect.right;

        // console.log('resize');
        // console.log("right: " + parseInt(right, 10));

        return right.toString();        
    });
};