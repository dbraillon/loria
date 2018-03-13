task add

    Create a task 

    -name [name]
        A value that specifies the name of the scheduled task to create

    -desc [desc]
        A value that specifies the description of the scheduled task to create

    -schedule [schedule]
        A value that specifies the schedule frequency. Valid values are: 
            MINUTE, HOURLY, DAILY, WEEKLY, MONTHLY, ANNUAL, ONCE

    -modifier [modifier]
        A value that refines the schedule type to allow for finer control over the schedule recurrence. Valid values are: 
            MINUTE: 1 - 1439 minutes
            HOURLY: 1 - 23 hours
            DAILY: 1 - 365 days
            WEEKLY: weeks 1 - 52
            ANNUAL: weeks 1 - 99
            ONCE: No modifiers

    -daysOfWeek [day]
        A value that specifies the day of the week to run the task. Valid values are: 1-7, *

    -daysOfMonth [day]
        A value that specifies the day of the week to run the task. Valid values are: 1-31, *

    -monthsOfYear [month]
        A value that specifies months of the year. Valid values are: 1-12, *

    -starttime HH:mm
        A value that specifies the start time to run the task

    -endtime HH:mm
        A value that specifies the end time to run the task

    -startdate yyyyMMdd
        A value that specifies the first date on which to run the task

    -enddate yyyyMMdd
        A value that specifies the last date that the task will run

    -delete
        A value that marks the task to be deleted after its final run

    -help -?        
        A value that displays help for ADD


task delete         
    
    Delete one or more scheduled tasks

    -all            
        A value that deletes every scheduled tasks
    
    -name [name]    
        A value that specifies the name of the scheduled task to delete

    -help -?        
        A value that displays help for DELETE


task run

    Immediately run a scheduled task

    -name [name]
        A value that specifies the name of the scheduled task to run

    -help -?
        A value that displays help for RUN


task get
    
    Get informations about scheduled tasks

    -all
        A value that gets every scheduled tasks

    -name [name]
        A value that specifies the name of the scheduled task to get

    -help -?
        A value that displays help for GET


task help

    Get informations about task module
