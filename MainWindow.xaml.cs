using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TP4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// !!!! NE PAS METTRE DE CODE ICI !!!! 

    public partial class MainWindow : Window
    {
        // #############################
        // #### VARIABLES GLOBALES #####
        // Ces variables sont accessible partout dans le code arrière.
        /// <summary>
        /// Permet de savoir si permettant de charger les données à été cliquer pour permettre l'accès au autre fonction du code
        /// </summary>
        bool btnChargementCsvCliquer = false;

        /// <summary>
        /// Indique si des modifications sont en cours sur les données d'un employé
        /// </summary>
        bool modifEnCours = false;

        /// <summary>
        /// Chemin d'accès de la nouvelle image
        /// </summary>
        string cheminNouvelleImage = null;

        /// <summary>
        /// Permet de savoir partour dans le code quel employer est présentement sélectionné
        /// </summary>
        int employerSelectionner = 0;

        /// <summary>
        ///  La matrice à utiliser pour récupérer les données extraites votre fichier .CSV
        /// </summary>
        public string[,] matrice;

        /// <summary>
        /// Chemin d'accès vers le fichier CSV à lire.
        /// </summary>
        string cheminAccesLecture = @"C:\data\420-04A-FX\TP4\TP4_SAMSON.csv";

        public MainWindow()
        {
            // NE PAS RETIRER CE CODE
            InitializeComponent();
            DatePckNaissance.Text = DateTime.Now.ToString();

            // Ici, c'est comme votre programme "Main" : vous pouvez coder essentiellement 
            // l'initialisation des vos variables, des champs par défaut de formulaire (si le cas),
            // Attention, vous n'avez pas à coder les appels des fonctions événements de l'interface graphique 
            // car celles-ci sont lancées par l'interration de l'utilisateur avec le programme (ex. click, sélection, etc)

        }

        /// <summary>
        /// Événement qui va essentiellement lancer la fonction LireCsvChargerMatrice avec le bon chemin d'accès, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnChargerFichier_Click(object sender, RoutedEventArgs e)
        {
            // CODE FOURNI
            // La fonction LireCsvChargerMatrice doit être complétée par vous.

            // Charger la liste déroulante de votre formulaire. Cette liste doit contenir les noms de vos entités
            // TODO

            //Si le bouton de chargement n'a pas été appuyé
            if (!btnChargementCsvCliquer)
            {
                //Appel de la fonction pour charger la matrice
                matrice = LireCsvChargerMatrice(cheminAccesLecture);
                cmbEmployer.Items.Clear();

                //Boucle pour parcourir les lignes de la matrice
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    //Boucle pour parcourir les colonnes de la matrice
                    for (int j = 0; j < 1; j++)
                    {
                        //Ajout des items dans la liste déroulante
                        cmbEmployer.Items.Add($"{i + 1}. {matrice[i, 1]}");
                    }
                }

                // Par défaut, la première entitée doit être sélectionnée dans la liste et son contenu doit être affiché dans le formulaire
                // TODO

                //Valeur par défaut de la liste déroulante à 0
                cmbEmployer.SelectedIndex = 0;

                //Rendre visible le bouton pour sauvegarder et quitter
                BtnSauvegarderQuitter.Visibility = Visibility.Visible;

                //Rendre visible l'espace recherche
                StkRecherche.Visibility = Visibility.Visible;

                //Afficher le bouton modification
                BtnModifier.Visibility = Visibility.Visible;

                //Message indiquant que les donées ont été chargées
                MessageBox.Show("Données chargées", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Information);
                
                btnChargementCsvCliquer = true;
            }
            //Sinon afficher le message indiquant que les données ont déjà été chargées
            else
            {
                MessageBox.Show("Données déjà chargées...", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #region VOS FONCTIONS 

        //########################## VOS FONCTIONS ###############################
        //########################################################################
        //C'est à partir d'ici que vous coder votre propres fonctions, nécessaires pour votre TP.


        /// <summary>
        ///  Fonction responsable de lire le contenu d'un fichier CSV, de retirer la dernière ligne vide et de retourner
        ///  une matrice de strings où chaque case représente une ligne du fichier, EXCLUANT l'entête.
        /// </summary>
        /// <param name="cheminAcces">Chemin d'accès vers le fichier CSV</param>
        /// <returns>Une matrice contenant les lignes du fichier, EXCLUANT l'entête. </returns>
        static string[,] LireCsvChargerMatrice(string cheminAcces)
        {

            #region CODE FOURI
            // Ouvrir le fichier CSV
            StreamReader fichierEntree = new StreamReader(cheminAcces);

            // LIRE tout le fichier, INCLUANT la ligne d'entête
            string contenuFichier = fichierEntree.ReadToEnd();
            contenuFichier = contenuFichier.Replace("\r", "");
            string[] lignesFichierTemporaire = contenuFichier.Split('\n');
            string[] lignesFichier;

            // Si la dernière ligne est vide, on l'exclue dans le vecteur de lignes
            if (lignesFichierTemporaire[lignesFichierTemporaire.Length - 1] == "")
            {
                // On dimensionne le vecteur de ligne pour exclure  2 lignes (l'entête et la dernière ligne vide)
                lignesFichier = new string[lignesFichierTemporaire.Length - 2];
            }
            else
            {   // On dimensionne le vecteur de ligne pour exclure  1 ligne (l'entête)
                lignesFichier = new string[lignesFichierTemporaire.Length - 1];
            }

            // Remplir le vecteur de lignes avec les lignes du fichier, incluant l'entête.
            for (int i = 0; i < lignesFichier.Length; i++)
            {
                // [i+1] On commence à la ligne 1, car la ligne 0 est l'entête
                lignesFichier[i] = lignesFichierTemporaire[i + 1];
            }
            fichierEntree.Close();
            #endregion



            #region À COMPLÉTER

            /* ##### COMPLÉTER LE RESTE DE LA FONCTION ###### */
            /* ############################################## */

            // Trouver le nombre de lignes et de colonnes du fichier, afin de préparer
            // la construction de la matrice de strings
            // TODO

            //Nb de colonnes dans le fichier
            int nbColonnes = lignesFichier[0].Split(';').Length;

            //Nb de lignes dans le fichier
            int nbLignes = lignesFichier.Length;

            // Création de la matrice : à modifier avec la bonne dimension
            // TODO
            string[,] employees = new string[nbLignes, nbColonnes];

            // Affectation de la matrice à partir des données de lignesFichier; 
            // TODO

            for (int i = 0; i < lignesFichier.Length; i++)
            {
                //Création d'un vecteur temporaire
                string[] vecteurProprieter = lignesFichier[i].Split(';');

                //Boucle pour parcourir cette boucle
                for (int j = 0; j < vecteurProprieter.Length; j++)
                {
                    //Affectation des données du vecteur à la matrice
                    employees[i, j] = vecteurProprieter[j];
                }
            }
            #endregion

            // Retourner une matrice
            return employees;

        }

        /// <summary>
        /// Événement lorsque la sélection est changée dans la liste déroulante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmployer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Affectation de l'index sélectionner à la variable global employerSelectionner
            employerSelectionner = cmbEmployer.SelectedIndex;

            //Appel de la fonction pour cacher le menu de modification
            CacherControleModification(sender, e);

            //Appel de la fonction affichant les information de l'employer sélectionnées
            AfficherInfo(sender,e, -1);
        }

        /// <summary>
        /// Fonctions qui affectent les valeurs aux éléments prévus à cet effet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ID">Entier représentent l'ID de l'employé (-1) signale que la valeur ne sera pas utilsée</param>
        private void AfficherInfo(object sender, RoutedEventArgs e, int ID)
        {
            //Si la valeur de la liste déroulante n'est pas null
            if (cmbEmployer.SelectedValue != null)
            {
                //Si l'ID est plus grand que 0
                if (ID < 0)
                {
                    //Création d'une variable de travail pour y stocker la valeur sélectionnée
                    string selectedValue = cmbEmployer.SelectedValue.ToString();

                    //Boucle parcourant les lignes de la matrice
                    for (int i = 0; i < matrice.GetLength(0); i++)
                    {
                        //Si l'ID de la matrice est égal à l'ID de l'employé sélectionné
                        if (employerSelectionner == Convert.ToInt32(matrice[i, 0]))
                        {
                            //Affichage du nom de l'employé
                            TxtBlockPrenom.Text = selectedValue.Substring(3).Trim();

                            //Affichage du nom de famille de l'employé
                            TxtBlockNom.Text = matrice[i, 2].ToString();

                            //Affiche de l'ID de l'employé
                            TxtBlockID.Text = matrice[i, 0];

                            //Affichage de la date de naissance de l'employé
                            DatePckNaissance.Text = matrice[i, 9];

                            //Affichage de l'âge de l'employé à l'aide la fonction calculant l'âge
                            TxtBlockAge.Text = $"({CalculAge(DatePckNaissance.Text, i)} ans)";

                            //Affichage du salair annuel de l'employé
                            TxtBoxSalaire.Text = FormatageText(matrice[i, 4], "argent");

                            //Affichage du poste de l'employé
                            TxtBoxPoste.Text = matrice[i, 5];

                            //Affichage de l'année d'ancienneté de l'employé
                            TxtBoxAnciennete.Text = matrice[i, 6];

                            //Variable de travail pour obtenir le dossier de l'application permettant d'accéder au dossier "img" 
                            string imagePath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));

                            //Affichage de la nouvelle image
                            imgEmployer.Source = new BitmapImage(new Uri(imagePath + "\\img\\" + matrice[i, 8], UriKind.Absolute));
                            imgEmployer.Source.Freeze();

                            //Affichage du numéro de téléphone de l'employé
                            TxtBoxCellulaire.Text = FormatageText(matrice[i, 10], "téléphone");

                            //Affichage du numéro de téléphone du contact d'urgence de l'employé
                            TxtBoxContactUrgence.Text = FormatageText(matrice[i, 11], "téléphone");

                            //Boucle parcourant les items de la liste déroulante de contact d'urgence 
                            for (int k = 0; k < CmbLienAvecEmployer.Items.Count; k++)
                            {
                                //Si la valeur dans la matrice est égale à un des items
                                if (matrice[i, 12] == CmbLienAvecEmployer.Items[k].ToString().Substring(CmbLienAvecEmployer.Items[k].ToString().Length - matrice[i, 12].Length))
                                {
                                    //Afficher cet item qui correspond au lien du contact d'urgence avec l'employé 
                                    CmbLienAvecEmployer.SelectedIndex = k;
                                }
                            }

                            //Si la valeur est oui
                            if (matrice[i, 7].ToLower() == "oui")
                            {
                                //Cocher le bouton radio oui
                                OptAugmentationOui.IsChecked = true;
                            }
                            //Sinon si la valeur est égal à non
                            else if (matrice[i, 7].ToLower() == "non")
                            {
                                //Cocher le bouton radio non
                                OptAugmentationNon.IsChecked = true;
                            }
                        }
                    }

                }

                //Sinon si l'ID est supérieur ou égal à 0
                else if (ID >= 0)
                {
                    //Affichage de la date de naissance de l'employé
                    DatePckNaissance.Text = matrice[ID, 9];

                    //Affichage de l'âge de l'employé à l'aide la fonction calculant l'âge
                    TxtBlockAge.Text = $"({CalculAge(DatePckNaissance.Text, ID)} ans)";

                    //Affichage du salair annuel de l'employé
                    TxtBoxSalaire.Text = FormatageText(matrice[ID, 4], "argent");

                    //Affichage du poste de l'employé
                    TxtBoxPoste.Text = matrice[ID, 5];

                    //Affichage de l'année d'ancienneté de l'employé
                    TxtBoxAnciennete.Text = matrice[ID, 6];

                    //Variable de travail pour obtenir le dossier de l'application permettant d'accéder au dossier "img" 
                    string imagePath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));

                    //Affichage de la nouvelle image
                    imgEmployer.Source = new BitmapImage(new Uri(imagePath + "\\img\\" + matrice[ID, 8], UriKind.Absolute));
                    imgEmployer.Source.Freeze();

                    //Affichage du numéro de téléphone de l'employé
                    TxtBoxCellulaire.Text = FormatageText(matrice[ID, 10], "téléphone");

                    //Affichage du numéro de téléphone du contact d'urgence de l'employé
                    TxtBoxContactUrgence.Text = FormatageText(matrice[ID, 11], "téléphone");

                    //Boucle parcourant les items de la liste déroulante de contact d'urgence 
                    for (int k = 0; k < CmbLienAvecEmployer.Items.Count; k++)
                    {
                        //Si la valeur dans la matrice est égale à un des items
                        if (matrice[ID, 12] == CmbLienAvecEmployer.Items[k].ToString().Substring(CmbLienAvecEmployer.Items[k].ToString().Length - matrice[ID, 12].Length))
                        {
                            //Afficher cet item qui correspond au lien du contact d'urgence avec l'employé 
                            CmbLienAvecEmployer.SelectedIndex = k;
                        }
                    }

                    //Si la valeur est oui
                    if (matrice[ID, 7].ToLower() == "oui")
                    {
                        //Cocher le bouton radio oui
                        OptAugmentationOui.IsChecked = true;
                    }
                    //Sinon si la valeur est égal à non
                    else if (matrice[ID, 7].ToLower() == "non")
                    {
                        //Cocher le bouton radio non
                        OptAugmentationNon.IsChecked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Événement qui se produit quand le bouton modifié est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            //Si les deux valeurs sont à vrai
            if (btnChargementCsvCliquer && !modifEnCours)
            {
                //Indique que nous sommes en cour de modification
                modifEnCours = true;

                //Cacher le bouton sauvegarder et quitter
                StkSauvegarderQuitter.Visibility = Visibility.Hidden;

                //Afficher la valeur du salaire en numérique
                TxtBoxSalaire.Text = matrice[employerSelectionner, 4];

                //Affichage du numéro de téléphone sans les parenthèses, espaces et tiret à l'aide de la fonction FormatageText
                TxtBoxCellulaire.Text = FormatageText(TxtBoxCellulaire.Text, "modifTel");

                //Affichage du numéro de téléphone du contact d'urgence sans les paranthèse, espaces et tiret à l'aide de la fonction FormatageText
                TxtBoxContactUrgence.Text = FormatageText(TxtBoxContactUrgence.Text, "modifTel");

                //Rendre modifiable la date de naissance
                DatePckNaissance.IsEnabled = true;

                //Rendre modifialbe le salaire
                TxtBoxSalaire.IsEnabled = true;

                //Rendre modifiable le numéro de téléphone
                TxtBoxCellulaire.IsEnabled = true;

                //Rendre modifiable la liste déroulante du lien du contact d'urgence
                CmbLienAvecEmployer.IsEnabled = true;

                //Rendre modifiable le numéro de téléphone du contact d'urgence
                TxtBoxContactUrgence.IsEnabled = true;

                //Rendre modifiable le poste
                TxtBoxPoste.IsEnabled = true;

                //Rendre modifiable le bouton radio de l'augmentation de salaire
                OptAugmentationOui.IsEnabled = true;

                //Rendre modifiable le bouton radio de l'augmentation de salaire
                OptAugmentationNon.IsEnabled = true;

                //Rendre modifiable l'année d'ancienneté
                TxtBoxAnciennete.IsEnabled = true;

                //Ajouter un arrière plan jaune pour chacun des éléments pouvant être édité
                DatePckNaissance.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                TxtBoxSalaire.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                TxtBoxCellulaire.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                TxtBoxContactUrgence.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                CmbLienAvecEmployer.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                TxtBoxPoste.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                StkOptAugmentation.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };
                TxtBoxAnciennete.Background = new SolidColorBrush(Colors.Yellow) { Opacity = 0.5 };

                //Ajout d'un bordure jaune autour de la date de naissance
                DatePckNaissance.BorderBrush = new SolidColorBrush(Colors.Yellow) { Opacity = 1 };

                //Rendre visible les boutons annuler, sauvegarder et vider le formulaire
                StkSaveCancel.Visibility = Visibility.Visible;

                //Rendre visible un message indiquant que des modifications sont en cour
                TxtBlockModification.Visibility = Visibility.Visible;
            }
            //Sinon si des modifications sont en cour
            else if (modifEnCours)
            {
                //Afficher un message de confirmation pour continuer les modifications ou les annuler
                MessageBoxResult result = MessageBox.Show("Quittez le mode de modification\n\nVous perdrez tous les changements apportés\n\nConfirmer...", "EquiGuest", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

                //Switch pour effectuer l'action en fonction de la réponse de l'utilisateur
                switch (result)
                {
                    //Si le choix est oui (annuler modification)
                    case MessageBoxResult.Yes:
                        //Appel de la fonction pour cacher les contrôles de modification
                        CacherControleModification(sender, e);
                        break;
                        //Si le choix est non (ne pas annuler les modifications)
                    case MessageBoxResult.No:
                        //Ne rien faire
                        break;
                }
            }
        }

        /// <summary>
        /// Événement qui se produit quand le bouton annuler est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //Message de confirmation pour l'annulation des modifications
            MessageBoxResult result = MessageBox.Show("Êtes-vous certain de vouloir annuler vos modifications ?", "EquiGuest", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            //Switch pour effectuer une action selon l'action de l'utilisateur
            switch (result)
            {
                //Si oui (Annuler modification)
                case MessageBoxResult.Yes:
                    //Appel de la fonctions pour annuler les modifications
                    AnnulerModification(sender, e);
                    //Appel de la fonction pour cacher les contrôles de modification
                    CacherControleModification(sender, e);
                    break;

                //Si le choix est non (ne pas annuler)
                case MessageBoxResult.No:
                    //Ne rien faire
                    break;
            }
        }

        /// <summary>
        /// Fonctions cachant les contrôles de modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CacherControleModification(object sender, RoutedEventArgs e)
        {
            //Indique qu'il n'y a plus de modifications
            modifEnCours = false;

            //Rendre visible le bouton sauvegarder et quitter
            StkSauvegarderQuitter.Visibility = Visibility.Visible;

            //Cacher les boutons sauvegarder, annuler et vider le formulaire
            StkSaveCancel.Visibility = Visibility.Hidden;

            //Cacher le message indiquant que des modifications sont en cour
            TxtBlockModification.Visibility = Visibility.Hidden;

            //Rendre la date de naissance non modifiable
            DatePckNaissance.IsEnabled = false;

            //Rendre le salaire non modifiable
            TxtBoxSalaire.IsEnabled = false;

            //Rendre le numéro de téléphone non modifiable
            TxtBoxCellulaire.IsEnabled = false;

            //Rendre le lien du contact d'urgence non modifiable
            CmbLienAvecEmployer.IsEnabled = false;

            //Rendre le numéro de téléphone du contact d'urgence non modifiable
            TxtBoxContactUrgence.IsEnabled = false;

            //Rendre le poste non modifiable
            TxtBoxPoste.IsEnabled = false;

            //Rendre l'année d'ancienneté non modifiable
            TxtBoxAnciennete.IsEnabled = false;

            //Rendre les boutons radio pour l'augmentation de salaire non modifiables
            OptAugmentationNon.IsEnabled = false;
            OptAugmentationOui.IsEnabled = false;

            //Retirer l'arrière plan jaune de tous les éléments modifiables
            DatePckNaissance.Background = null;
            TxtBoxSalaire.Background = null;
            TxtBoxCellulaire.Background = null;
            TxtBoxContactUrgence.Background = null;
            CmbLienAvecEmployer.Background = null;
            TxtBoxPoste.Background = null;
            StkOptAugmentation.Background = null;
            TxtBoxAnciennete.Background = null;

            //Retirer la bordure jaune de la date de naissance
            DatePckNaissance.BorderBrush = null;
        }

        /// <summary>
        /// Événement qui se produit lorsqu'un clique se produit sur l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgEmployer_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Si les modifications sont activé
            if (modifEnCours)
            {
                //Création de l'explorateur de fichier
                OpenFileDialog ImageEmployer = new OpenFileDialog();

                //Appliquer un filtre uniquement pour les fichier jpg/jpeg et png
                ImageEmployer.Filter = "Image files (*.jpg;*.png;*jpeg)|*.jpg; *.png; *jpeg|All Files (*.*)|*.*";

                //Définir l'espace de fichier par défaut
                ImageEmployer.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Ouvir l'explorateur de fichier
                ImageEmployer.ShowDialog();

                //Si l'utilisateur a appuyé sur "Ok"
                if (ImageEmployer.ShowDialog() == true)
                {
                    //Enregistrer le chemin de l'image sélectionner
                    cheminNouvelleImage = ImageEmployer.FileName;

                    //Définir la source de l'élément image pour le chemin de la nouvelle image
                    imgEmployer.Source = new BitmapImage(new Uri(cheminNouvelleImage, UriKind.Absolute));
                    imgEmployer.Source.Freeze();
                }
            }
        }

        /// <summary>
        /// Fonctions annulant les modifications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnulerModification(object sender, RoutedEventArgs e)
        {
            //variable de travail pour stocker l'employe sélectionné
            int ID = cmbEmployer.SelectedIndex;

            //Appel de la fonction pour réafficher les valeurs de base
            AfficherInfo(sender, e, ID);
        }

        /// <summary>
        /// Quand la souris entre dans l'élément image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgEmployer_MouseEnter(object sender, MouseEventArgs e)
        {
            //Si les modifications son active
            if (modifEnCours)
            {
                //Changer le curseur pour la main qui pointe
                Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// Quand la souris quitte l'élément image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgEmployer_MouseLeave(object sender, MouseEventArgs e)
        {
            //Changer le curseur pour la flèche
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Événement qui se produit quand le bouton sauvegarder est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            //Boucle parcourant toutes les lignes de la matrice
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                //Si l'index de l'employé est égal à l'index dans la matrice
                if (cmbEmployer.SelectedIndex == Convert.ToInt32(matrice[i, 0]))
                {
                    //Affecter la nouvelle date de naissance à l'aide de la fonction CalculAge()
                    matrice[i, 3] = CalculAge(DatePckNaissance.Text, i);

                    //Affecter le nouveau salaire
                    matrice[i, 4] = TxtBoxSalaire.Text;
                    matrice[i, 5] = TxtBoxPoste.Text;
                    matrice[i, 6] = TxtBoxAnciennete.Text;

                    if (OptAugmentationOui.IsChecked == true)
                    {
                        matrice[i, 7] = "oui";
                    }
                    else if (OptAugmentationNon.IsChecked == true)
                    {
                        matrice[i, 7] = "non";
                    }

                    if (modifEnCours && !string.IsNullOrEmpty(cheminNouvelleImage))
                    {
                        string directory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin")) + "img\\";
                        string cheminMatrice = cheminNouvelleImage;

                        // Déchargez l'élément Image
                        imgEmployer.Source = null;

                        // Copiez l'image vers la destination
                        if (!File.Exists(directory + Path.GetFileName(cheminMatrice)))
                        {
                            File.Copy(cheminNouvelleImage, Path.Combine(directory, Path.GetFileName(cheminNouvelleImage)), true);
                            matrice[i, 8] = Path.GetFileName(cheminNouvelleImage).ToString();
                        }

                    }
                    matrice[i, 9] = DatePckNaissance.Text;
                    matrice[i, 10] = TxtBoxCellulaire.Text;
                    matrice[i, 11] = TxtBoxContactUrgence.Text;
                    matrice[i, 12] = CmbLienAvecEmployer.Text;

                }
            }
            cmbEmployer.SelectedIndex++;
            cmbEmployer.SelectedIndex--;
        }

        /// <summary>
        /// Fonctions pour calculer l'âge de l'employé quand la date de naissance a été modifié
        /// </summary>
        /// <param name="dateDeNaissance">Chaine de caractère contenant la date de naissance</param>
        /// <param name="ID">Entier avec le ID de l'employé</param>
        /// <returns>Retourne l'âge de l'employé</returns>
        public string CalculAge(string dateDeNaissance, int ID)
        {
            //Vecteur temporaire
            string[] vectTemporaire = dateDeNaissance.Split('-');

            //Vecteur qui comportera la date de naissance spéraré en trois
            int[] date = new int[vectTemporaire.Length];

            //Boucle parcourant le vecteur temporaire
            for (int i = 0; i < date.Length; i++)
            {
                //Affectation des valeurs convertie dans le vecteur date
                date[i] = Convert.ToInt32(vectTemporaire[i]);
            }

            //L'année actuelle - la date de naissance
            int age = DateTime.Now.Year - date[0];

            //Retourne l'âge de l'employé
            return Convert.ToString(age);
        }

        /// <summary>
        /// Fonction pour formater un texte dans un format donnée
        /// </summary>
        /// <param name="text">chaine de charctère à formater</param>
        /// <param name="type">Le type de formatage à effectuer</param>
        /// <returns>Retourne la chaine de caractère formattée dans le format désiré</returns>
        public static string FormatageText(string text, string type)
        {
            //Switch pour faire le formatage selon le type donné
            switch (type)
            {
                case "téléphone":
                    return String.Format("{0:(###) ###-####}", Convert.ToInt64(text));
                case "argent":
                    return String.Format("{0:C}", Convert.ToInt32(text));
                case "modifTel":
                    return text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                //Si aucun type n'est donné ou le type ne correspond avec aucun type disponible, il retourne le texte non formaté
                default:
                    return text;
            }
        }

        /// <summary>
        /// Événement qui se produit lorsque le bouton Quittez est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'un message de confirmation
            MessageBoxResult result = MessageBox.Show("Quittez sans sauvegarder.\n\nVous perdrez toutes les modifications apportées\n\nConfirmer...", "EquiGuest", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
            
            //Switch pour effectuer l'action requise
            switch (result)
            {
                //Si oui (Quitter)
                case MessageBoxResult.Yes:
                    //Fermer l'application
                    Environment.Exit(0);
                    break;
                //Si non (Ne pas quitter)
                case MessageBoxResult.No:
                    //Ne rien faire
                    break;
            }
        }

        /// <summary>
        /// Événement qui se produit lorsque le bouton sauvegarder et cliquer et cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSauvegarderQuitter_Click(object sender, RoutedEventArgs e)
        {
            //Définir le chemin d'accès d'écriture
            string cheminAccesEcriture = @"C:\data\420-04A-FX\TP4\TP4_SAMSON.csv";

            StreamWriter ecritureFichierCsv = new StreamWriter(cheminAccesEcriture);

            //Écriture de l'en-tête
            ecritureFichierCsv.Write("ID;NOM;NOMFAMILLE;AGE;SALAIRE;POSTE;ANCIENNETE;augmentationDeSalaire;IMG;Date de naissance;Cellulaire;ContactUrgence;LienAvecEmployerContactUrgence\n");
            //Si la matrice n'est null (vide)
            if (matrice != null)
            {
                //Boucle parcourant toutes les lignes de la matrice
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    //Ériture des données dans le fichier CSV
                    ecritureFichierCsv.Write($"{matrice[i, 0]};{matrice[i, 1]};{matrice[i, 2]};{matrice[i, 3]};{matrice[i, 4]};{matrice[i, 5]};{matrice[i, 6]};{matrice[i, 7]};{matrice[i, 8]};{matrice[i, 9]};{matrice[i, 10]};{matrice[i, 11]};{matrice[i, 12]}\r\n");
                }
            }

            //Fermeture du fichier
            ecritureFichierCsv.Close();

            //Message qui indique que la sauvegarde a été complété
            MessageBox.Show("Sauvegarde complétée,", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Information);

            //Fermeture de l'application
            Environment.Exit(0);
        }

        /// <summary>
        /// Événement qui se produit lorsque le bouton pour vider le formulaire est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViderFormulaire_Click(object sender, RoutedEventArgs e)
        {
            //Mettre tous les champs de texte à null (vide)
            TxtBoxCellulaire.Text = null;
            TxtBoxContactUrgence.Text = null;
            CmbLienAvecEmployer.Text = null;
            TxtBoxAnciennete.Text = null;
            TxtBoxSalaire.Text = null;
            TxtBoxPoste.Text = null;
        }

        /// <summary>
        /// Événement quand le bouton rechercher est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRechercher_Click(object sender, RoutedEventArgs e)
        {
            //Variable de travail pour stocker la recherche
            string recherche = TxtBoxRecherche.Text;

            //Nombre de résultat trouvé avec la recherche
            int resultatTrouver = 0;

            //Effacer tous les items de la liste d'item
            ListBoxRecherche.Items.Clear();

            //Boucle parcourant toutes les lignes de la matrice
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                //Boucle parcourant toutes les colonnes de la matrice
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    //Si la lignes i et la colonnes j corresponde à la recherche
                    if (matrice[i, j] == recherche)
                    {
                        //Ajouter 1
                        resultatTrouver++;

                        //Ajouter l'employé au item de la list d'item
                        ListBoxRecherche.Items.Add($"{Convert.ToInt32(matrice[i,0])+1}. {matrice[i,2]}, {matrice[i,1]}");
                    }
                }
            }

            //Si le nombre de résultat trouvé est supérieur à 0
            if (resultatTrouver > 0)
            {
                //Rendre la liste d'item visible
                ListBoxRecherche.Visibility = Visibility.Visible;
                //Message indiquant le nombre de résultat trouvé
                MessageBox.Show($"{resultatTrouver} enregistrement trouvé", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Sinon
            else
            {
                //Message indiquant aucun résultat n'a été trouvé
                MessageBox.Show("Aucun résultat trouvé", "EquiGuest", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        /// <summary>
        /// Quand un item de la listbox est double cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxRecherche_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Si la sélection n'est pas null
            if (!(ListBoxRecherche.SelectedItem == null))
            {
                //Transformer le string pour retenir uniquement le numéro
                string rechercheSelectionner = ListBoxRecherche.SelectedItem.ToString().Substring(0, 2).Replace(".", "").Trim();
                
                //Le convertir en entier et lui soustraire 1
                int ID = Convert.ToInt32(rechercheSelectionner)-1;

                //Modifier l'index sélectionné de la liste déroulante pour afficher l'employé sélectionné
                cmbEmployer.SelectedIndex = ID;
            }
        }
        #endregion
    }
}