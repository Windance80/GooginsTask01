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
};