param(
    [Parameter(Mandatory=$False)]
    [bool]$Clean = $false,

    [Parameter(Mandatory=$False)]
    [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
    [string]$Verbosity = "normal"
)



function Log-Info{
    param(
        [string]$Message
    )
    $currentTime = Get-Date -Format "HH:mm:ss"
    $message = "[Inf] [{0}] - {1}" -f $currentTime, $Message
    Write-Host $message -ForegroundColor Blue
}

function Log-Success{
    param(
        [string]$Message
    )
    $currentTime = Get-Date -Format "HH:mm:ss"
    $message = "[Suc] [{0}] - {1}" -f $currentTime, $Message
    Write-Host $message -ForegroundColor Green
}

function Log-Warning{
    param(
        [string]$Message
    )
    $currentTime = Get-Date -Format "HH:mm:ss"
    $message = "[Wrn] [{0}] - {1}" -f $currentTime, $Message
    Write-Host $message -ForegroundColor Yellow
}
function Log-Error{
    param(
        [string]$Message
    )

    $currentTime = Get-Date -Format "HH:mm:ss"
    $message = "[Err] [{0}] - {1}" -f $currentTime, $Message
    Write-Host $message -ForegroundColor Red
}

function HandleCommandExecutionError{
    param([string]$failMessage)

    if ($LASTEXITCODE -ne 0) {
        Log-Error($failMessage)

        if($null -ne $DBcontainerId){
            docker stop $DBcontainerId
            Log-Info("Database container stopped")
        }

        if ($null -ne $appContainerId){ 
            docker stop $appContainerId
            Log-Info("Api container stopped")
        }

        exit $LASTEXITCODE
    }
}

# Variables
$DBcontainerId = $null
$appContainerId = $null

# Paths
$solutionName = "FinancialSummary.sln"
$mainProjectRelativePath = "Source\FinancialSummary.Api\FinancialSummary.Api.csproj"
$solutionPath = "{0}\{1}" -f $PSScriptRoot, $solutionName
$mainProjectPath = "{0}\{1}" -f $PSScriptRoot, $mainProjectRelativePath

# Clean Migrations
if($Clean){
    Log-Warning("Clean option is enabled - Removing old migrations")
    dotnet ef migrations remove -s $mainProjectPath 

    HandleCommandExecutionError("Migration clean failed")
}


# Clean solution
if($Clean){
    Log-Warning("Cleaning solution")
    dotnet clean $solutionPath -v n
    Log-Success("Solution cleaned")
}

if($Clean){
    Log-Warning("Clean option is enabled - Forcing restore")
    dotnet restore -s $mainProjectPath --force -v $Verbosity
}
else{
    Log-Info("Restoring solution")
    dotnet restore -s $mainProjectPath --force -v $Verbosity
}

HandleCommandExecutionError("Restoring solution failed")

Log-Success("Solution restored")

# Build Project

Log-Info("Building project")

dotnet build $mainProjectPath -c Release -v $Verbosity --no-restore

HandleCommandExecutionError("Building project failed")

Log-Success("Project built successfully")

# Pull database image

docker compose pull financialsummary.database

HandleCommandExecutionError("Postgres image pull failed")

Log-Success("Postgres pulled successfully")

# Create containers

docker compose create financialsummary.api --no-recreate

HandleCommandExecutionError("Containers creation failed")

Log-Success("Financial Summary Api and Postgre Database containers created")

# Run database container

$DBcontainerId = docker ps -a -q --filter "name=FinancialSummaryDatabase"

Log-Info("Database Container ID: $DBcontainerId")

$_ = docker start $DBcontainerId 

HandleCommandExecutionError("Failed to run Postgres container")

Log-Success("Postgres container is running")

# Migrations

$Output = dotnet ef migrations add InitialMigration --project $mainProjectRelativePath

if($Output -eq "The name 'InitialMigration' is used by an existing migration."){
    Log-Warning("Initial migration already exists. Aborting migration.")
    $_ = docker stop $DBContainerId
    Log-Info("Database container stopped")
    exit $LASTEXITCODE
}
else{
    Log-Success("Initial migration created")
}


# Run API container

$appContainerId = docker ps -a -q --filter "name=FinancialSummaryApp"

Log-Info("FinancialSummary Container ID: $appContainerId")

$_ = docker start $appContainerId 

HandleCommandExecutionError("Migration creation failed")

Log-Success("FinancialSummary container is running")


# Finalizing
$_ = docker stop $DBContainerId
Log-Info("Database container stopped")

$_ = docker stop $appContainerId
Log-Info("FinancialSummary container stopped")