# azure-function-webhook-example
Example for webhook implementation in azure functions

It is a demonstration for how to use web hooks and how to run a basic azure c# function. In this example, if any resources listening by azure application insights goes offline, it is calling this azure function which simply creating for a web request for slack incoming web hook and post a simple warning.
This example is also useful for how to integrate with slack api, and how to do basic messaging(text, formattings, icons etc.)