


-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'onlineexam-dev')
BEGIN
    CREATE DATABASE [onlineexam-dev];
END
GO

-- Use the database
USE [onlineexam-dev];
GO



-- Create Tables
-- Table: ApplicationUsers
CREATE TABLE [ApplicationUsers] (
[Id] nvarchar(450) NOT NULL,
[FirstName] nvarchar(255) NOT NULL,
[LastName] nvarchar(255) NOT NULL,
[UserName] nvarchar(256),
[NormalizedUserName] nvarchar(256),
[Email] nvarchar(256),
[NormalizedEmail] nvarchar(256),
[EmailConfirmed] bit NOT NULL,
[PasswordHash] nvarchar(255),
[SecurityStamp] nvarchar(255),
[ConcurrencyStamp] nvarchar(255),
[PhoneNumber] nvarchar(255),
[PhoneNumberConfirmed] bit NOT NULL,
[TwoFactorEnabled] bit NOT NULL,
[LockoutEnd] datetimeoffset,
[LockoutEnabled] bit NOT NULL,
[AccessFailedCount] int NOT NULL
, CONSTRAINT PK_ApplicationUsers PRIMARY KEY ([Id])
);

-- Table: AspNetRoleClaims
CREATE TABLE [AspNetRoleClaims] (
[Id] int NOT NULL IDENTITY(1,1),
[RoleId] nvarchar(450) NOT NULL,
[ClaimType] nvarchar(255),
[ClaimValue] nvarchar(255)
, CONSTRAINT PK_AspNetRoleClaims PRIMARY KEY ([Id])
);

-- Table: AspNetRoles
CREATE TABLE [AspNetRoles] (
[Id] nvarchar(450) NOT NULL,
[Name] nvarchar(256),
[NormalizedName] nvarchar(256),
[ConcurrencyStamp] nvarchar(255)
, CONSTRAINT PK_AspNetRoles PRIMARY KEY ([Id])
);

-- Table: AspNetUserClaims
CREATE TABLE [AspNetUserClaims] (
[Id] int NOT NULL IDENTITY(1,1),
[UserId] nvarchar(450) NOT NULL,
[ClaimType] nvarchar(255),
[ClaimValue] nvarchar(255)
, CONSTRAINT PK_AspNetUserClaims PRIMARY KEY ([Id])
);

-- Table: AspNetUserLogins
CREATE TABLE [AspNetUserLogins] (
[LoginProvider] nvarchar(450) NOT NULL,
[ProviderKey] nvarchar(450) NOT NULL,
[ProviderDisplayName] nvarchar(255),
[UserId] nvarchar(450) NOT NULL
, CONSTRAINT PK_AspNetUserLogins PRIMARY KEY ([LoginProvider], [ProviderKey])
);

-- Table: AspNetUserRoles
CREATE TABLE [AspNetUserRoles] (
[UserId] nvarchar(450) NOT NULL,
[RoleId] nvarchar(450) NOT NULL
, CONSTRAINT PK_AspNetUserRoles PRIMARY KEY ([UserId], [RoleId])
);

-- Table: AspNetUserTokens
CREATE TABLE [AspNetUserTokens] (
[UserId] nvarchar(450) NOT NULL,
[LoginProvider] nvarchar(450) NOT NULL,
[Name] nvarchar(450) NOT NULL,
[Value] nvarchar(255)
, CONSTRAINT PK_AspNetUserTokens PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

-- Table: ExamResults
CREATE TABLE [ExamResults] (
[ExamResultId] int NOT NULL IDENTITY(1,1),
[UserExamId] int NOT NULL,
[TotalObtainedMarks] decimal(18,2) NOT NULL,
[ResultStatus] nvarchar(255) NOT NULL,
[CreatedBy] nvarchar(255) NOT NULL,
[CreatedOn] datetime2 NOT NULL,
[UpdatedOn] datetime2 NOT NULL
, CONSTRAINT PK_ExamResults PRIMARY KEY ([ExamResultId])
);

-- Table: Exams
CREATE TABLE [Exams] (
[ExamId] int NOT NULL IDENTITY(1,1),
[Title] nvarchar(255) NOT NULL,
[Description] nvarchar(255) NOT NULL,
[StartDate] datetime2 NOT NULL,
[EndDate] datetime2 NOT NULL,
[Duration] float NOT NULL,
[TotalQuestions] int NOT NULL,
[TotalMarks] decimal(18,2) NOT NULL,
[PassingMarks] decimal(18,2) NOT NULL,
[IsRandomized] bit NOT NULL,
[IsActive] bit NOT NULL,
[CreatedBy] nvarchar(255) NOT NULL,
[CreatedOn] datetime2 NOT NULL,
[UpdatedOn] datetime2 NOT NULL
, CONSTRAINT PK_Exams PRIMARY KEY ([ExamId])
);

-- Table: Options
CREATE TABLE [Options] (
[OptionId] int NOT NULL IDENTITY(1,1),
[QuestionId] int NOT NULL,
[OptionText] nvarchar(255) NOT NULL,
[IsCorrect] bit NOT NULL,
[Marks] decimal(18,2) NOT NULL
, CONSTRAINT PK_Options PRIMARY KEY ([OptionId])
);

-- Table: Questions
CREATE TABLE [Questions] (
[QuestionId] int NOT NULL IDENTITY(1,1),
[SectionId] int NOT NULL,
[QuestionText] nvarchar(255) NOT NULL,
[IsMedia] bit NOT NULL,
[MediaType] nvarchar(255) NOT NULL,
[MediaURL] nvarchar(255) NOT NULL,
[IsMultipleChoice] bit NOT NULL,
[IsFromQuestionBank] bit NOT NULL,
[QuestionMaxMarks] decimal(18,2) NOT NULL,
[CreatedBy] nvarchar(255) NOT NULL,
[CreatedOn] datetime2 NOT NULL,
[UpdatedOn] datetime2 NOT NULL
, CONSTRAINT PK_Questions PRIMARY KEY ([QuestionId])
);

-- Table: SectionResults
CREATE TABLE [SectionResults] (
[SectionResultId] int NOT NULL IDENTITY(1,1),
[SectionId] int NOT NULL,
[UserExamId] int NOT NULL,
[QuestionsAttempted] int NOT NULL,
[MarksObtained] decimal(18,2) NOT NULL,
[ResultStatus] nvarchar(255) NOT NULL
, CONSTRAINT PK_SectionResults PRIMARY KEY ([SectionResultId])
);

-- Table: Sections
CREATE TABLE [Sections] (
[SectionId] int NOT NULL IDENTITY(1,1),
[ExamId] int NOT NULL,
[Title] nvarchar(255) NOT NULL,
[TotalQuestions] int NOT NULL,
[TotalMarks] decimal(18,2) NOT NULL,
[PassingMarks] decimal(18,2) NOT NULL,
[WeightagePercentage] decimal(18,2) NOT NULL,
[CreatedBy] nvarchar(255) NOT NULL,
[CreatedOn] datetime2 NOT NULL,
[UpdatedOn] datetime2 NOT NULL
, CONSTRAINT PK_Sections PRIMARY KEY ([SectionId])
);

-- Table: UserAnswerOptions
CREATE TABLE [UserAnswerOptions] (
[UserAnswerId] int NOT NULL,
[OptionId] int NOT NULL
, CONSTRAINT PK_UserAnswerOptions PRIMARY KEY ([UserAnswerId], [OptionId])
);

-- Table: UserAnswers
CREATE TABLE [UserAnswers] (
[UserAnswerId] int NOT NULL IDENTITY(1,1),
[QuestionId] int NOT NULL,
[UserExamId] int NOT NULL,
[SectionId] int NOT NULL
, CONSTRAINT PK_UserAnswers PRIMARY KEY ([UserAnswerId])
);

-- Table: UserExams
CREATE TABLE [UserExams] (
[UserExamId] int NOT NULL IDENTITY(1,1),
[UserId] nvarchar(450) NOT NULL,
[ExamId] int NOT NULL,
[StartedOn] datetime2 NOT NULL,
[FinishedOn] datetime2,
[ExamStatus] nvarchar(255) NOT NULL,
[TotalMarks] decimal(18,2),
[IsAutoSubmitted] bit,
[NoOfAttempt] int NOT NULL,
[CreatedBy] nvarchar(255) NOT NULL,
[CreatedOn] datetime2 NOT NULL,
[UpdatedOn] datetime2 NOT NULL
, CONSTRAINT PK_UserExams PRIMARY KEY ([UserExamId])
);



-- Add Relationships
ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [AspNetRoleClaimRoleIdfk] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE Cascade;
ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [AspNetUserClaimUserIdfk] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUsers]([Id]) ON DELETE Cascade;
ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [AspNetUserLoginUserIdfk] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUsers]([Id]) ON DELETE Cascade;
ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [AspNetUserRoleUserIdfk] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUsers]([Id]) ON DELETE Cascade;
ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [AspNetUserRoleRoleIdfk] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE Cascade;
ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [AspNetUserTokenUserIdfk] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUsers]([Id]) ON DELETE Cascade;
ALTER TABLE [ExamResults] ADD CONSTRAINT [ExamResultUserExamIdfk] FOREIGN KEY ([UserExamId]) REFERENCES [UserExams]([UserExamId]) ON DELETE No Action;
ALTER TABLE [Options] ADD CONSTRAINT [OptionQuestionIdfk] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([QuestionId]) ON DELETE No Action;
ALTER TABLE [Questions] ADD CONSTRAINT [QuestionSectionIdfk] FOREIGN KEY ([SectionId]) REFERENCES [Sections]([SectionId]) ON DELETE No Action;
ALTER TABLE [SectionResults] ADD CONSTRAINT [SectionResultSectionIdfk] FOREIGN KEY ([SectionId]) REFERENCES [Sections]([SectionId]) ON DELETE No Action;
ALTER TABLE [SectionResults] ADD CONSTRAINT [SectionResultUserExamIdfk] FOREIGN KEY ([UserExamId]) REFERENCES [UserExams]([UserExamId]) ON DELETE No Action;
ALTER TABLE [Sections] ADD CONSTRAINT [SectionExamIdfk] FOREIGN KEY ([ExamId]) REFERENCES [Exams]([ExamId]) ON DELETE No Action;
ALTER TABLE [UserAnswerOptions] ADD CONSTRAINT [UserAnswerOptionUserAnswerIdfk] FOREIGN KEY ([UserAnswerId]) REFERENCES [UserAnswers]([UserAnswerId]) ON DELETE No Action;
ALTER TABLE [UserAnswerOptions] ADD CONSTRAINT [UserAnswerOptionOptionIdfk] FOREIGN KEY ([OptionId]) REFERENCES [Options]([OptionId]) ON DELETE No Action;
ALTER TABLE [UserAnswers] ADD CONSTRAINT [UserAnswerQuestionIdfk] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([QuestionId]) ON DELETE No Action;
ALTER TABLE [UserAnswers] ADD CONSTRAINT [UserAnswerUserExamIdfk] FOREIGN KEY ([UserExamId]) REFERENCES [UserExams]([UserExamId]) ON DELETE No Action;
ALTER TABLE [UserAnswers] ADD CONSTRAINT [UserAnswerSectionIdfk] FOREIGN KEY ([SectionId]) REFERENCES [Sections]([SectionId]) ON DELETE No Action;
ALTER TABLE [UserExams] ADD CONSTRAINT [UserExamUserIdfk] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUsers]([Id]) ON DELETE No Action;
ALTER TABLE [UserExams] ADD CONSTRAINT [UserExamExamIdfk] FOREIGN KEY ([ExamId]) REFERENCES [Exams]([ExamId]) ON DELETE No Action;


-- Create Indices
CREATE  INDEX [EmailIndex] ON [ApplicationUsers] (NormalizedEmail);
CREATE UNIQUE INDEX [UserNameIndex] ON [ApplicationUsers] (NormalizedUserName);
CREATE  INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] (RoleId);
CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] (NormalizedName);
CREATE  INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] (UserId);
CREATE  INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] (UserId);
CREATE  INDEX [IX_ExamResults_UserExamId] ON [ExamResults] (UserExamId);
CREATE  INDEX [IX_Options_QuestionId] ON [Options] (QuestionId);
CREATE  INDEX [IX_Questions_SectionId] ON [Questions] (SectionId);
CREATE  INDEX [IX_SectionResults_SectionId] ON [SectionResults] (SectionId);
CREATE  INDEX [IX_SectionResults_UserExamId] ON [SectionResults] (UserExamId);
CREATE  INDEX [IX_Sections_ExamId] ON [Sections] (ExamId);
CREATE  INDEX [IX_UserAnswers_QuestionId] ON [UserAnswers] (QuestionId);
CREATE  INDEX [IX_UserAnswers_SectionId] ON [UserAnswers] (SectionId);
CREATE  INDEX [IX_UserAnswers_UserExamId] ON [UserAnswers] (UserExamId);
CREATE  INDEX [IX_UserExams_ExamId] ON [UserExams] (ExamId);
CREATE  INDEX [IX_UserExams_UserId] ON [UserExams] (UserId);




