CREATE DATABASE [FastLearningDB] 
go
USE [FastLearningDB]
GO
/****** Object:  Table [dbo].[Clase]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clase](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_habilidad] [int] NOT NULL,
	[descripcion] [varchar](1024) NULL,
	[lugar] [varchar](256) NULL,
	[dia] [varchar](50) NULL,
	[hora_inicial] [time](0) NOT NULL,
	[hora_final] [time](0) NOT NULL,
	[fecha_inicial] [datetime] NOT NULL,
	[fecha_final] [datetime] NOT NULL,
	[precio] [money] NOT NULL,
	[vacantes] [int] NOT NULL,
	[fecha_registro] [datetime] NULL,
	[id_estado] [int] NOT NULL CONSTRAINT [DF_Clase_id_estado]  DEFAULT ((1)),
 CONSTRAINT [PK_Clase] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClaseUsuario]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaseUsuario](
	[id_clase] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[calificacion_alumno] [int] NULL,
	[calificacion_profesor] [int] NULL,
	[id_pago] [int] NULL,
 CONSTRAINT [PK_ClaseUsuario] PRIMARY KEY CLUSTERED 
(
	[id_clase] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Curso]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Curso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](1024) NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Descuento]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Descuento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](25) NOT NULL,
	[numero] [decimal](3, 2) NOT NULL,
	[valor] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Descuento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DescuentoUsuario]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DescuentoUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_descuento] [int] NOT NULL,
	[estado_eliminado] [int] NOT NULL CONSTRAINT [DF_DescuentoUsuario_estado]  DEFAULT ((1)),
	[estado_utilizado] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_DescuentoUsuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estado]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[id] [int] NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Habilidad]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habilidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_tema] [int] NOT NULL,
	[id_nivel] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[estado] [int] NOT NULL CONSTRAINT [DF_Habilidad_estado]  DEFAULT ((1)),
 CONSTRAINT [PK_Habilidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Nivel]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Nivel](
	[id] [int] NOT NULL,
	[descripcion] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Nivel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[monto_inicial] [money] NOT NULL,
	[id_descuento_usuario] [int] NULL,
	[monto_final] [money] NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Pago] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Solicitud]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Solicitud](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](1024) NULL,
	[id_usuario] [int] NOT NULL,
	[fecha_registro] [datetime] NULL,
	[estado] [int] NOT NULL CONSTRAINT [DF_Solicitud_estado]  DEFAULT ((1)),
	[id_curso] [int] NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SolicitudUsuario]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SolicitudUsuario](
	[id_solicitud] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[descripcion] [varchar](256) NOT NULL,
	[fecha_registro] [datetime] NULL,
 CONSTRAINT [PK_SolicitudUsuario] PRIMARY KEY CLUSTERED 
(
	[id_solicitud] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tema]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tema](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](1024) NULL,
	[id_curso] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Tema] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/06/2018 9:06:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NULL,
	[apellidos] [varchar](50) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](50) NOT NULL,
	[contrasena] [varchar](50) NOT NULL,
	[fecha_registro] [datetime] NULL,
	[rol] [int] NOT NULL CONSTRAINT [DF_Usuario_rol]  DEFAULT ((1)),
	[calificacion_alumno] [int] NULL CONSTRAINT [DF_Usuario_calificacion_alumno]  DEFAULT ((5)),
	[calificacion_profesor] [int] NULL CONSTRAINT [DF_Usuario_calificacion_profesor]  DEFAULT ((5)),
	[puntos] [int] NOT NULL CONSTRAINT [DF_Usuario_puntos]  DEFAULT ((0)),
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Clase]  WITH CHECK ADD  CONSTRAINT [FK_Clase_Estado] FOREIGN KEY([id_estado])
REFERENCES [dbo].[Estado] ([id])
GO
ALTER TABLE [dbo].[Clase] CHECK CONSTRAINT [FK_Clase_Estado]
GO
ALTER TABLE [dbo].[Clase]  WITH CHECK ADD  CONSTRAINT [FK_Clase_Habilidad] FOREIGN KEY([id_habilidad])
REFERENCES [dbo].[Habilidad] ([id])
GO
ALTER TABLE [dbo].[Clase] CHECK CONSTRAINT [FK_Clase_Habilidad]
GO
ALTER TABLE [dbo].[ClaseUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ClaseUsuario_Clase] FOREIGN KEY([id_clase])
REFERENCES [dbo].[Clase] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClaseUsuario] CHECK CONSTRAINT [FK_ClaseUsuario_Clase]
GO
ALTER TABLE [dbo].[ClaseUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ClaseUsuario_Pago] FOREIGN KEY([id_pago])
REFERENCES [dbo].[Pago] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClaseUsuario] CHECK CONSTRAINT [FK_ClaseUsuario_Pago]
GO
ALTER TABLE [dbo].[ClaseUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ClaseUsuario_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClaseUsuario] CHECK CONSTRAINT [FK_ClaseUsuario_Usuario]
GO
ALTER TABLE [dbo].[DescuentoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_DescuentoUsuario_Descuento] FOREIGN KEY([id_descuento])
REFERENCES [dbo].[Descuento] ([id])
GO
ALTER TABLE [dbo].[DescuentoUsuario] CHECK CONSTRAINT [FK_DescuentoUsuario_Descuento]
GO
ALTER TABLE [dbo].[DescuentoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_DescuentoUsuario_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DescuentoUsuario] CHECK CONSTRAINT [FK_DescuentoUsuario_Usuario]
GO
ALTER TABLE [dbo].[Habilidad]  WITH CHECK ADD  CONSTRAINT [FK_Habilidad_Nivel] FOREIGN KEY([id_nivel])
REFERENCES [dbo].[Nivel] ([id])
GO
ALTER TABLE [dbo].[Habilidad] CHECK CONSTRAINT [FK_Habilidad_Nivel]
GO
ALTER TABLE [dbo].[Habilidad]  WITH CHECK ADD  CONSTRAINT [FK_Habilidad_Tema] FOREIGN KEY([id_tema])
REFERENCES [dbo].[Tema] ([id])
GO
ALTER TABLE [dbo].[Habilidad] CHECK CONSTRAINT [FK_Habilidad_Tema]
GO
ALTER TABLE [dbo].[Habilidad]  WITH CHECK ADD  CONSTRAINT [FK_Habilidad_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Habilidad] CHECK CONSTRAINT [FK_Habilidad_Usuario]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_DescuentoUsuario] FOREIGN KEY([id_descuento_usuario])
REFERENCES [dbo].[DescuentoUsuario] ([id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_DescuentoUsuario]
GO
ALTER TABLE [dbo].[Solicitud]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_Curso] FOREIGN KEY([id_curso])
REFERENCES [dbo].[Curso] ([id])
GO
ALTER TABLE [dbo].[Solicitud] CHECK CONSTRAINT [FK_Solicitud_Curso]
GO
ALTER TABLE [dbo].[Solicitud]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Solicitud] CHECK CONSTRAINT [FK_Solicitud_Usuario]
GO
ALTER TABLE [dbo].[SolicitudUsuario]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudUsuario_Solicitud] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[Solicitud] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolicitudUsuario] CHECK CONSTRAINT [FK_SolicitudUsuario_Solicitud]
GO
ALTER TABLE [dbo].[SolicitudUsuario]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudUsuario_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[SolicitudUsuario] CHECK CONSTRAINT [FK_SolicitudUsuario_Usuario]
GO
ALTER TABLE [dbo].[Tema]  WITH CHECK ADD  CONSTRAINT [FK_Tema_Curso] FOREIGN KEY([id_curso])
REFERENCES [dbo].[Curso] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tema] CHECK CONSTRAINT [FK_Tema_Curso]
GO

